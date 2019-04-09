using Marketplace.Model.Abstracts;
using Marketplace.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Model.Models
{
    public class User : BaseUserEntity<int>
    {
        public string LockoutReason { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        
    }
}
