using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberlerProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin»FullYonetim,Admin»AltYonetici")]
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}