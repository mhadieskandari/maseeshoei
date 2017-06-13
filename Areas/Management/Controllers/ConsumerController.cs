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
    public class ConsumerController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Consumers/

        public ActionResult Index()
        {
            return View(db.T_Consumers.ToList());
        }

        


        //
        // GET: /Consumers/Details/5

        public ActionResult Details(int id = 0)
        {
            T_Consumers t_consumers = db.T_Consumers.Find(id);
            if (t_consumers == null)
            {
                return HttpNotFound();
            }
            return View(t_consumers);
        }

        //
        // GET: /Consumers/Create

        public ActionResult Create()
        {

            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName",1);
            return View();
        }

        //
        // POST: /Consumers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_Consumers t_consumers)
        {
            if (ModelState.IsValid)
            {
                db.T_Consumers.Add(t_consumers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", 1);
            return View(t_consumers);
        }

        //
        // GET: /Consumers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            T_Consumers t_consumers = db.T_Consumers.Find(id);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_consumers.StateID);
            if (t_consumers == null)
            {
                return HttpNotFound();
            }
            return View(t_consumers);
        }

        //
        // POST: /Consumers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_Consumers t_consumers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_consumers).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName",t_consumers.StateID);
            return View(t_consumers);
        }

        //
        // GET: /Consumers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            T_Consumers t_consumers = db.T_Consumers.Find(id);
            if (t_consumers == null)
            {
                return HttpNotFound();
            }
            return View(t_consumers);
        }

        //
        // POST: /Consumers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Consumers t_consumers = db.T_Consumers.Find(id);
            db.T_Consumers.Remove(t_consumers);
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