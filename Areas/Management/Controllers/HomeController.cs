﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaaseShooei.Areas.Management.Controllers
{
    [Authorize(Roles = "HighUser,Administrator")]
    public class HomeController : Controller
    {
        //
        // GET: /Management/Home/

        
        public ActionResult Index()
        {
            return View();
        }

    }
}
