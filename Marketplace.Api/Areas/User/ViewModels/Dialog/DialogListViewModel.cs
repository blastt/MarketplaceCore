using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.ViewModels
{
    public class DialogListViewModel
    {
        public IEnumerable<DialogViewModel> Dialogs { get; set; }
        public int CountOfInbox { get; set; }
        public int CountOfUnread { get; set; }
    }
}
