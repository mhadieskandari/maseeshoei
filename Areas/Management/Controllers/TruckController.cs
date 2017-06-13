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
    [Authorize(Roles = "HighUser,Administrator")]
    public class TruckController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Truck/

        public ActionResult Index()
        {
            return View(db.T_Trucks.ToList());
        }

        //
        // GET: /Truck/Details/5

        public ActionResult Details(int id = 0)
        {
            T_Trucks t_trucks = db.T_Trucks.Find(id);
            if (t_trucks == null)
            {
                return HttpNotFound();
            }
            return View(t_trucks);
        }

        //
        // GET: /Truck/Create

        public ActionResult Create()
        {
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName");
            return View();
        }

        //
        // POST: /Truck/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_Trucks t_trucks)
        {
            if (ModelState.IsValid)
            {
                db.T_Trucks.Add(t_trucks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName");
            return View(t_trucks);
        }

        //
        // GET: /Truck/Edit/5

        public ActionResult Edit(int id = 0)
        {
            T_Trucks t_trucks = db.T_Trucks.Find(id);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_trucks.StateID);
           
            if (t_trucks == null)
            {
                return HttpNotFound();
            }
            return View(t_trucks);
        }

        //
        // POST: /Truck/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_Trucks t_trucks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_trucks).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_trucks.StateID);
           
            return View(t_trucks);
        }

        //
        // GET: /Truck/Delete/5

        public ActionResult Delete(int id = 0)
        {
            T_Trucks t_trucks = db.T_Trucks.Find(id);
            if (t_trucks == null)
            {
                return HttpNotFound();
            }
            return View(t_trucks);
        }

        //
        // POST: /Truck/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Trucks t_trucks = db.T_Trucks.Find(id);
            db.T_Trucks.Remove(t_trucks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}