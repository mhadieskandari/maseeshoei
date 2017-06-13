using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaaseShooei.Controllers
{
    [Description("خانه")]
    public class HomeController : Controller
    {
        [Description("صفحه اصلی")]
        public ActionResult Index()
        {
            Session["DBConnection"] = "";
           return RedirectToAction("index","management/BurdenInformation");
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //return View();
        }
        [Description("درباره ما")]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Description("تماس با ما")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
