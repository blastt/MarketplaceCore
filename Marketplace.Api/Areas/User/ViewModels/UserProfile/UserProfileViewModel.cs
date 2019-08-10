using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.ViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar32 { get; set; }
        public string Avatar64 { get; set; }
        public string Avatar96 { get; set; }
    }
}
