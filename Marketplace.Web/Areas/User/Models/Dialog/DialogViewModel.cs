﻿using Marketplace.Web.Areas.User.Models.Message;
using Marketplace.Web.Areas.User.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Areas.User.Models.Dialog
{
    public class DialogViewModel
    {
        public int Id { get; set; }

        public int CountOfNewMessages { get; set; }

        public int CreatorId { get; set; }
        public UserProfileViewModel Creator { get; set; }

        public int CompanionId { get; set; }
        public UserProfileViewModel Companion { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }
    }
}
