using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Web.Models.Offer;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(SearchOfferViewModel searchInfo)
        {
            return View(searchInfo);
        }
    }
}