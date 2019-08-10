using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Api.Areas.User.ViewModels;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Api.Areas.User.Controllers
{
    [Route("api/user/[controller]")]
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

        [HttpGet]
        public async Task<DialogListViewModel> Inbox()
        {
            //include: source => source.Include(i => i.Game).Include(i => i.UserProfile)
            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            var dialogs = await dialogService.GetUserDialogsAsync(currentUserId, include: source => source.Include(i => i.Companion).Include(i => i.Creator).Include(i => i.Messages));
            var dialogsUnread = await dialogService.GetUserDialogsAsync(currentUserId, d => d.Messages.Any(m => !m.ToViewed), include: source => source.Include(i => i.Messages));
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
            return model;
        }

        public async Task<ViewResult> Unread()
        {

            int currentUserId = await userService.GetCurrentUserId(HttpContext.User);
            var dialogs = await dialogService.GetUserDialogsAsync(currentUserId, d => d.Messages.Any(m => !m.ToViewed));
            var dialogsInbox = await dialogService.GetUserDialogsAsync(currentUserId, include: source => source.Include(i => i.Messages));
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
                Dialog dialog = await dialogService.GetDialogAsync(d => d.Id == id.Value, include: source => source.Include(i => i.Creator).Include(i => i.Companion).Include(i => i.Messages));
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