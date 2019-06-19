using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Areas.Admin.Controllers
{
    public class FilterRangeValueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}