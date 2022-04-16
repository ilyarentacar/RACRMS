using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Controllers
{
    public class CommingSoonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
