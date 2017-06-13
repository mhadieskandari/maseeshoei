using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaseShooei.Areas.Management.Models;

namespace MaaseShooei.Areas.Management.Controllers
{
    [Authorize(Roles = "HighUser,Administrator,Truck,Consumer,User")]
    //[Authorize(Roles = "HighUser,Administrator")]
    public class UserController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /User/
         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Index()
        {
            return View(db.T_Users.ToList());
        }

        //
        // GET: /User/Details/5
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Details(int id = 0)
        {
            T_Users t_users = db.T_Users.Find(id);
            if (t_users == null)
            {
                return HttpNotFound();
            }
            return View(t_users);
        }

        //
        // GET: /User/Create
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.T_Users_Type.ToList(), "TypeID", "TypeName",1);
            ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);

            return View();
        }

        //
        // POST: /User/Create
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_Users t_users, FormCollection frm)
        {
            if (ModelState.IsValid)
            {

                if (t_users.TypeID == 1)
                {

                }
                else if (t_users.TypeID == 2)
                {
                    t_users.PublicID =int.Parse( frm["TruckID"].ToString());
                    T_UserRoles role = db.T_UserRoles.Where(m=>m.RoleName=="Truck").ToList()[0];
                    T_UsersInRoles uir = new T_UsersInRoles();
                    //uir.UserId = t_users.UserID;
                    uir.RoleId = role.RoleId;
                    t_users.T_UsersInRoles.Add(uir);
                   
                }
                else if (t_users.TypeID == 3)
                {
                    t_users.PublicID = int.Parse(frm["ConsumerID"].ToString());
                    T_UserRoles role = db.T_UserRoles.Where(m => m.RoleName == "Consumer").ToList()[0];
                    T_UsersInRoles uir = new T_UsersInRoles();
                    //uir.UserId = t_users.UserID;
                    uir.RoleId = role.RoleId;
                    t_users.T_UsersInRoles.Add(uir);              
                }

                db.T_Users.Add(t_users);                
                db.SaveChanges();             


                TempData["Msg"] = "کابر ایجاد شد";
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.T_Users_Type.ToList(), "TypeID", "TypeName", 1);
            ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);

            return View(t_users);
        }

        //
        // GET: /User/Edit/5
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Edit( int id = 0)
        {
            T_Users t_users = db.T_Users.Find(id);
            ViewBag.TypeID = new SelectList(db.T_Users_Type.ToList(), "TypeID", "TypeName", t_users.TypeID);
            if(t_users.TypeID==2){
                ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", t_users.PublicID);
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);
                
            }
            else if (t_users.TypeID == 3)
            {
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", t_users.PublicID);
                ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);

            }
            else
            {
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);
                ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);
            
            }
           
            if (t_users == null)
            {
                return HttpNotFound();
            }
            return View(t_users);
        }

        //
        // POST: /User/Edit/5
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_Users t_users,FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                
                if (t_users.TypeID == 1)
                {
                    t_users.PublicID = null;
                    db.Entry(t_users).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (t_users.TypeID == 2)
                {
                    t_users.PublicID =int.Parse( frm["TruckID"].ToString());
                    db.Entry(t_users).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (t_users.TypeID == 3)
                {
                    t_users.PublicID = int.Parse(frm["ConsumerID"].ToString());
                    db.Entry(t_users).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {

                }

                ViewBag.TypeID = new SelectList(db.T_Users_Type.ToList(), "TypeID", "TypeName", t_users.TypeID);
                if (t_users.TypeID == 2)
                {
                    ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", t_users.PublicID);
                    ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);

                }
                else if (t_users.TypeID == 3)
                {
                    ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", t_users.PublicID);
                    ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);

                }
                else
                {
                    ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);
                    ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);

                }

               





                TempData["Msg"] = "کابر اصلاح شد";
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.T_Users_Type.ToList(), "TypeID", "TypeName", t_users.TypeID);

            


            if (t_users.TypeID == 2)
            {
                ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", t_users.PublicID);
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);

            }
            else if (t_users.TypeID == 3)
            {
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", t_users.PublicID);
                ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);

            }
            else
            {
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", 1);
                ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName", 1);

            }

            return View(t_users);
        }

        
         //GET: /User/Delete/5
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Delete(int id = 0)
        {
            T_Users t_users = db.T_Users.Find(id);
            if (t_users == null)
            {
                return HttpNotFound();
            }
            return View(t_users);
        }

        
         //POST: /User/Delete/5
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Users t_users = db.T_Users.Find(id);
            List<T_UsersInRoles> roles = t_users.T_UsersInRoles.ToList();
            for (int i = 0; i < roles.Count; i++) 
            {
                t_users.T_UsersInRoles.Remove(roles[i]);
            }
                


            db.T_Users.Remove(t_users);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult UserAccess(int id=0)
        {
            if (id == 0) {

                return HttpNotFound();
            }
            T_Users t_user = db.T_Users.Find(id);
            List<int> ur = new List<int>();
            for (int i = 0; i < t_user.T_UsersInRoles.ToList().Count; i++)
            {
                ur.Add(t_user.T_UsersInRoles.ToList()[i].RoleId);
            }
            List<T_UserRoles> ex = db.T_UserRoles.Where(m => !ur.Contains(m.RoleId)).ToList();

            ViewBag.RoleID = new SelectList(ex, "RoleID", "RoleName");
            ViewBag.Roles = t_user.T_UsersInRoles.ToList();
            return View(t_user);
            
        }
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost, ActionName("UserAccess")]
        [ValidateAntiForgeryToken]
        public ActionResult UserAccessAdd( FormCollection frm)
        {

            T_Users t_user = db.T_Users.Find(int.Parse(frm["UserID"].ToString()));

            if (int.Parse(frm["RoleID"].ToString()) != 0 )
            {
                
                int roleid = int.Parse(frm["RoleID"].ToString());
                if (roleid != 0)
                {
                    T_UsersInRoles role = new T_UsersInRoles();
                    role.RoleId = roleid;                    
                    t_user.T_UsersInRoles.Add(role);
                    db.Entry(t_user).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("UserAccess", new {id=t_user.UserID });
                }
            }
            // ViewBag.UserID =new SelectList(db.T_Users.ToList(), "UserI", "RoleName");
            List<T_UserRoles> ur=new List<T_UserRoles>();
            for (int i = 0; i < t_user.T_UsersInRoles.ToList().Count; i++)
            {
                ur.Add(db.T_UserRoles.Find(t_user.T_UsersInRoles.ToList()[i].RoleId));
            }
            
            ViewBag.RoleID = new SelectList(db.T_UserRoles.Except(ur).ToList(), "RoleID", "RoleName");
            ViewBag.Roles = db.T_Users.Find(int.Parse(frm["UserID"].ToString())).T_UsersInRoles.ToList();

            return View(t_user);
        }
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult DeleteRole(long id =0)
        {
            T_UsersInRoles t_userRole = db.T_UsersInRoles.Find(id);
            int uid=t_userRole.UserId;

            db.T_UsersInRoles.Remove(t_userRole);
            db.SaveChanges();

            return RedirectToAction("UserAccess", new { id = uid });
        }

        [Authorize(Roles = "HighUser,Administrator,Truck,Consumer,User")]
        public ActionResult EditUserOwn()
        {

            T_Users user = db.T_Users.Where(m=> m.UserName.Equals(User.Identity.Name)).ToList()[0];
            ViewBag.UserName = user.UserName;
            return View(user);
        }
        [Authorize(Roles = "HighUser,Administrator,Truck,Consumer,User")]
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserOwn(T_Users t_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_users).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.errs = "ویرایش شد";
                ViewBag.UserName = t_users.UserName;
                return View();
            }
            ViewBag.errf = "خطا در ویرایش";
            T_Users user = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0];
            ViewBag.UserName = user.UserName;
            return View(user);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}