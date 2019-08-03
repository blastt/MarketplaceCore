using AutoMapper;
using Marketplace.Model;
using Marketplace.Model.Models;
using Marketplace.Service.Services;
using Marketplace.Web.Areas.User.Models.Feedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.User.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IUserProfileService userProfileService;
        private readonly IFeedbackService feedbackService;
        private readonly IUserService userService;
        public FeedbackController(IUserProfileService userProfileService, IFeedbackService feedbackService, IUserService userService)
        {
            this.userProfileService = userProfileService;
            this.feedbackService = feedbackService;
            this.userService = userService;
        }

        public async Task<ActionResult> All()
        {
            var userId = await userService.GetCurrentUserId(HttpContext.User);
            var feedbacks = await feedbackService.GetFeedbacksAsync(f => f.UserToId == userId, include: source => source.Include(i => i.UserTo));
            var modelFeedbacks = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(feedbacks);
            var model = new FeedbackListViewModel()
            {
                Feedbacks = modelFeedbacks
            };
            return View(model);
        }

        public async Task<ActionResult> Positive()
        {
            var userId = await userService.GetCurrentUserId(HttpContext.User);
            var feedbacks = await feedbackService.GetFeedbacksAsync(f => f.UserToId == userId && f.Grade == FeedbackGrade.Good, include: source => source.Include(i => i.UserTo));
            var modelFeedbacks = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(feedbacks);
            var model = new FeedbackListViewModel()
            {
                Feedbacks = modelFeedbacks
            };
            return View(model);
        }

        public async Task<ActionResult> Negative()
        {
            var userId = await userService.GetCurrentUserId(HttpContext.User);
            var feedbacks = await feedbackService.GetFeedbacksAsync(f => f.UserToId == userId && f.Grade == FeedbackGrade.Bad, include: source => source.Include(i => i.UserTo));
            var modelFeedbacks = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(feedbacks);
            var model = new FeedbackListViewModel()
            {
                Feedbacks = modelFeedbacks
            };
            return View(model);
        }
    }
}
