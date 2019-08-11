﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.ViewModels
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