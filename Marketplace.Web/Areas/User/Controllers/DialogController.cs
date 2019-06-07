using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.User.Models.Dialog;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Areas.User.Controllers
{
    public class DialogController : Controller
    {
        private readonly IDialogService dialogService;
        private readonly IMessageService messageService;
        private readonly IUserProfileService userProfileService;
        private readonly IUserService userService;
        // GET: Offer
        public DialogController(IDialogService dialogService, IMessageService messageService, IUserProfileService userProfileService, IUserService userService)
        {
            this.messageService = messageService;
            this.dialogService = dialogService;
            this.userProfileService = userProfileService;
            this.userService = userService;
        }

        public async Task<ActionResult> Inbox()
        {
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            var dialogs = await dialogService.GetUserDialogsAsync(currentUserId, i => i.Companion, i => i.Creator, i => i.Messages);
            var dialogsUnread = await dialogService.GetUserDialogsAsync(currentUserId, d => d.Messages.Any(m => !m.ToViewed), i => i.Messages);
            var modelDialogs = new List<DialogViewModel>(); Mapper.Map<IEnumerable<Dialog>, IEnumerable<DialogViewModel>>(dialogs);

            foreach (var dialog in dialogs)
            {
                var dialogModel = Mapper.Map<Dialog, DialogViewModel>(dialog);
                dialogModel.CountOfNewMessages = dialog.Messages.Count(d => d.SenderId != currentUserId && !d.ToViewed);
                modelDialogs.Add(dialogModel);
            }
            var model = new DialogListViewModel()
            {
                Dialogs = modelDialogs,
                CountOfInbox = dialogs.Count,
                CountOfUnread = dialogsUnread.Count
            };
            return View(model);
        }

        public async Task<ViewResult> Unread()
        {

            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            var dialogs = await dialogService.GetUserDialogsAsync(currentUserId, d => d.Messages.Any(m => !m.ToViewed));
            var dialogsInbox = await dialogService.GetUserDialogsAsync(currentUserId, i => i.Messages);
            var modelDialogs = Mapper.Map<IEnumerable<Dialog>, IEnumerable<DialogViewModel>>(dialogs);
            var model = new DialogListViewModel()
            {
                Dialogs = modelDialogs,
                CountOfInbox = dialogsInbox.Count,
                CountOfUnread = dialogs.Count
            };
            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            int dialogWithUserId = 0;
            string dialogWithUserImage = null;
            if (id != null)
            {
                Dialog dialog = await dialogService.GetDialogAsync(d => d.Id == id.Value, i => i.Creator, i => i.Companion, i => i.Messages);
                if (dialog != null && ((await dialogService.GetUserDialogsAsync(currentUserId)).Count() != 0))
                {

                    if (dialog.CompanionId == currentUserId)
                    {
                        dialogWithUserId = dialog.CreatorId;
                        dialogWithUserImage = dialog.Creator.Avatar32;
                    }
                    else if (dialog.CreatorId == currentUserId)
                    {
                        dialogWithUserId = dialog.CompanionId;
                        dialogWithUserImage = dialog.Companion.Avatar32;
                    }

                    if (dialogWithUserId == 0)
                    {
                        return NotFound();
                    }
                    foreach (var message in dialog.Messages.Where(m => m.SenderId != currentUserId))
                    {
                        message.ToViewed = true;
                    }
                    await messageService.SaveMessageAsync();
                    var model = Mapper.Map<Dialog, DetailsDialogViewModel>(dialog);
                    model.OtherUserId = dialogWithUserId;
                    model.OtherUserImage = dialogWithUserImage;

                    return View(model);
                }
            }
            return NotFound();
        }


    }
}