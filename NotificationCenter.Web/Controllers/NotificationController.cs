using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NotificationCenter.Web.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admincollections()
        {
            return View();
        }

        public IActionResult GetCollectionsData()
        {
            return Json(new List<dynamic>() { new { id = 1, Name = "Test Collection", UsrCnt = 12 } });
        }
    }
}