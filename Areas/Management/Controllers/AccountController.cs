using MaaseShooei.Areas.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MaaseShooei.Areas.Management.Controllers
{
    public class AccountController : Controller
    {

        private MaaseDBEntities db;

        [HttpGet]
        public ActionResult LogOn(string returnUrl)
        {
            List<DataBase> dblist = new DataBase().GetDataDases();

            Session["DBConnection"] = "";
            Session["ReportConnection"] = "";
            Session["DBYearName"] = "";

            List<SelectListItem> list=new List<SelectListItem>();

            for (int i = 0; i < dblist.Count; i++)
            {
                SelectListItem s = new SelectListItem();
                s.Text = dblist[i].Text;
                s.Value = dblist[i].DBName;
                list.Add(s);
            }


            SelectList sl=new SelectList(list.OrderByDescending(m=>m.Value),"Value","Text");
            ViewBag.DBYear = sl;
                 
            if (User.Identity.IsAuthenticated) //remember me
            {
                
                if (shouldRedirect(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return Redirect(FormsAuthentication.DefaultUrl);
            }

            return View(); // show the login page
        }

        
        private bool LoginUser(string UserName, string PassWord)
        {
            bool hasUser = false;

            //var usr = from user in db.T_Users
            //          where user.UserName == UserName && user.PassWord == PassWord
            //          select user;
            //usr.ToList();

            List<T_Users> user = db.T_Users.Where(s => s.UserName == UserName && s.PassWord == PassWord).ToList();

            if (user.ToList().Count == 1)
            {
                hasUser = true;
               // Session["UserID"]= user[0]._UserID;
            }
            else
            {
                hasUser = false;
            }

            return hasUser;
        }



        [HttpGet]
        [Authorize(Roles = "HighUser,Administrator,User,LowUser,Accountant,Truck,Consumer")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
           // Session.Clear();
            //Session.RemoveAll();
            return Redirect(Request.UrlReferrer.ToString());
        }

        private bool shouldRedirect(string returnUrl)
        {

            bool flag =string.IsNullOrWhiteSpace(returnUrl) &&
                                Url.IsLocalUrl(returnUrl) &&
                                returnUrl.Length > 1 &&
                                returnUrl.StartsWith("/") &&
                                !returnUrl.StartsWith("//") &&
                                !returnUrl.StartsWith("/\\");
            // it's a security check
            return !flag;
        }

        [HttpPost]
        public ActionResult LogOn(Account loginInfo, string returnUrl,FormCollection frm)
        {



            if (frm["DBYear"].ToString() != "")
            {
                Session["DBConnection"] = frm["DBYear"].ToString();
                Session["ReportConnection"]= "report" + frm["DBYear"].ToString().Substring(15);
                Session["DBYearName"]= frm["DBYear"].ToString().Substring(15) + " سال";



                //Statics.dbconnection = frm["DBYear"].ToString();
                //Session["ReportConnection"].ToString() = "report"+frm["DBYear"].ToString().Substring(15);

                //Statics.DBYearName = frm["DBYear"].ToString().Substring(15)+" سال";
            }
            else
            {
                Session["DBYearName"] = "";
                Session["ReportConnection"] = "";
                Session["DBYearName"] = "";
            }
            string a = Session["DBYearName"].ToString();
            string b = Session["ReportConnection"].ToString();
            string c = Session["DBYearName"].ToString();

           db = new MaaseDBEntities();




            if (this.ModelState.IsValid)
            {
                if (LoginUser(loginInfo.Username, loginInfo.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginInfo.Username, loginInfo.RememberMe);
                    if (shouldRedirect(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    FormsAuthentication.RedirectFromLoginPage(loginInfo.Username, loginInfo.RememberMe);
                }
                
                   ViewBag.err= "نام کاربری یا رمز عبور اشتباه است";
                
            }


            List<DataBase> dblist = Statics.GetDataDases();
            List<SelectListItem> list = new List<SelectListItem>();

            for (int i = 0; i < dblist.Count; i++)
            {
                SelectListItem s = new SelectListItem();
                s.Text = dblist[i].Text;
                s.Value = dblist[i].DBName;
                list.Add(s);
            }


            SelectList sl = new SelectList(list.OrderByDescending(m => m.Value), "Value", "Text");
            ViewBag.DBYear = sl;

            //ViewBag.Error = "Login faild! Make sure you have entered the right user name and password!";
            return View(loginInfo);
        }
    }

}

