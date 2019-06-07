using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.User.Models.Message;
using Marketplace.Web.SignalRHubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.User.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageHub messageHub;
        private readonly IUserProfileService userProfileService;
        private readonly IUserService userService;
        private readonly IMessageService messageService;
        private readonly IOfferService offerService;
        private readonly IDialogService dialogService;

        public MessageController(IMessageService messageService, IOfferService offerService, IUserProfileService userProfileService, IDialogService dialogService, IUserService userService, IMessageHub messageHub)
        {
            this.messageHub = messageHub;
            this.messageService = messageService;
            this.offerService = offerService;
            this.userProfileService = userProfileService;
            this.dialogService = dialogService;
            this.userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> Create(MessageViewModel model)
        {


            if (ModelState.IsValid)
            {
                if (model.MessageBody.Trim() == "")
                {
                    return Json(new { success = false, responseText = "Вы не ввели сообщение" }, new Newtonsoft.Json.JsonSerializerSettings() { });
                }


                var toUser = userProfileService.GetUserProfile(u => u.Id == model.ReceiverId, u => u.DialogsAsСompanion, u => u.DialogsAsCreator,
                    u => u.DialogsAsCreator.Select(i => i.Messages), u => u.DialogsAsСompanion.Select(i => i.Messages));
                var currentUserId = await userService.GetCurrentUserId(HttpContext.User);
                var fromUser = await userProfileService.GetUserProfileAsync(u => u.Id == currentUserId, u => u.DialogsAsСompanion, u => u.DialogsAsCreator,
                    u => u.DialogsAsCreator.Select(i => i.Messages), u => u.DialogsAsСompanion.Select(i => i.Messages));
                if (toUser != null && fromUser != null && toUser.Id != fromUser.Id)
                {

                    Message message = Mapper.Map<MessageViewModel, Message>(model);
                    message.FromViewed = true;
                    message.SenderId = fromUser.Id;
                    message.CreatedDate = DateTime.Now;
                    var privateDialog = dialogService.GetPrivateDialog(toUser, fromUser);

                    if (privateDialog == null)
                    {
                        privateDialog = new Dialog()
                        {
                            CreatorId = fromUser.Id,
                            CompanionId = toUser.Id
                        };

                        dialogService.CreateDialog(privateDialog);
                        privateDialog.Messages.Add(message);
                        await messageService.SaveMessageAsync();

                        messageHub. AddDialog(fromUser.UserName, privateDialog.Id, fromUser.Avatar32, fromUser.Avatar32);
                        messageHub.AddDialog(toUser.UserName, privateDialog.Id, toUser.Avatar32, toUser.UserName);
                    }
                    else
                    {
                        privateDialog.Messages.Add(message);
                        await messageService.SaveMessageAsync();
                    }

                    int newDialogsCount = 0;
                    newDialogsCount = dialogService.UnreadDialogsForUserCount(toUser.Id);

                    messageHub.UpdateMessage(newDialogsCount, toUser.UserName);

                    int messageInDialogCount = 0;
                    messageInDialogCount = dialogService.UnreadMessagesInDialogCount(privateDialog);
                    var lastMessage = privateDialog.Messages.LastOrDefault();
                    if (lastMessage != null)
                    {
                        messageHub.UpdateMessageInDialog(messageInDialogCount, lastMessage.MessageBody, lastMessage.CreatedDate.ToShortDateString(), privateDialog.Id, toUser.UserName, toUser.Avatar32, fromUser.UserName);
                    }

                    messageHub.AddMessage(toUser.UserName, fromUser.UserName, message.MessageBody, message.CreatedDate.ToString(), fromUser.Avatar32);
                    return Json(new { success = true });
                }
                return Json(new { success = false, responseText = "Ошибка при отправке сообщения. Повторите попытку" });
            }

            return Json(new { success = false, responseText = "Ошибка при отправке сообщения. Повторите попытку" });
        }

        public async Task<int> GetUnreadDialogsCountAsync()
        {
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            int result = 0;
            int dialogsCount = dialogService.UnreadDialogsForUserCount(currentUserId);
            if (dialogsCount != 0)
            {
                result = dialogsCount;
            }

            return result;
        }
    }
}
