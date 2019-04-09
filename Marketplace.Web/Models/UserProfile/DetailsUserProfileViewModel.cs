using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Models.UserProfile
{
    public class DetailsUserProfileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar32 { get; set; }
        public string Avatar64 { get; set; }
        public string Avatar96 { get; set; }

        public UserProfileViewModel User { get; set; }
    }
}
