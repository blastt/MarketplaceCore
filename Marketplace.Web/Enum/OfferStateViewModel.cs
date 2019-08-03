using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web
{    
    public enum OfferStateViewModel
    {
        [Display(Name = "Active", ResourceType = typeof(SharedResource))]
        Active = OfferState.Active,

        [Display(Name = "Inactive", ResourceType = typeof(SharedResource))]
        Inactive = OfferState.Inactive,

        [Display(Name = "Closed", ResourceType = typeof(SharedResource))]
        Closed = OfferState.Closed
    }
}
