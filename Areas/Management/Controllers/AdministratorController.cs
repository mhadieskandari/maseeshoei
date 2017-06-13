using MaaseShooei.Areas.Management.Models;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MaaseShooei.Areas.Management.Controllers
{
    [Authorize(Roles = "HighUser,Administrator")]
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/

       // MaaseDBEntities db = new MaaseDBEntities();

       // public ActionResult Index()
       // {
       //     return RedirectToAction("Login");
       // }


       // public ActionResult Login()
       // {            
       //     return View();
       // }

       //[HttpPost]
       // public ActionResult Login(FormCollection form)
       // {
       //     if (ModelState.IsValid)
       //     {
       //         string UserName = Convert.ToString(form["_UserName"]);
       //         string PassWord = Convert.ToString(form["_PassWord"]);
       //         string rbm = Convert.ToString(form["RememberMe"]);

       //         if (rbm.Equals("true,false"))
       //         {
       //             rbm = rbm.Substring(0, rbm.IndexOf(','));
       //         }

       //         bool remmemberme = Convert.ToBoolean(rbm);
       //         if (LoginUser(UserName, PassWord))
       //         {
       //             return RedirectToAction("AdminPanel", "Administrator");
       //         }
       //         else
       //         {
                    
       //         }
                


       //     }
       //     return View();
       // }

       //private bool LoginUser(string UserName, string PassWord)
       //{
       //    bool hasUser = false;

       //    //var usr = from user in db.T_Users
       //    //          where user.UserName == UserName && user.PassWord == PassWord
       //    //          select user;
       //    //usr.ToList();

       //    List<T_Users> user = db.T_Users.Where(s => s.UserName == UserName && s.PassWord == PassWord).ToList();

       //    if (user.ToList().Count == 1)
       //    {
       //        hasUser = true;
       //        Session["UserID"] = user[0].UserID.ToString();
       //        Session["UserName"] = user[0].UserName.ToString();
       //        //Session["PassWord"] = user[0].UserName.ToString();
       //        Session["FullName"] = user[0].FirstName.ToString() + user[0].LastName.ToString();
       //        Session["UserType"] = user[0].UserType.ToString();
               
       //    }
       //    else
       //    {
       //        Session.RemoveAll();
       //    }


       //    return hasUser;
       //}


       // public ActionResult AdminPanel()
       // {


       //     if (Request.IsAuthenticated)
       //     {
       //         return RedirectToAction("AdminPanel");
       //     }
       //     else
       //     {
       //         return RedirectToAction("Login");
       //     }

       //     //if (Session["UserName"].ToString() != "")
       //     //{







       //     //}
       //     //else
       //     //{
       //     //    Session.RemoveAll();
       //     //    RedirectToAction("Login", "Administrator");
       //     //}
       //     return View();
       // }


        [Authorize]
        public ActionResult Index()
        {

            return View();
        }

    }
}
