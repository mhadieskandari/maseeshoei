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
    public class ProducerController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Producers/

        public ActionResult Index()
        {
            return View(db.T_Producers.ToList());
        }

        //
        // GET: /Producers/Details/5

        public ActionResult Details(int id = 0)
        {
            T_Producers t_producers = db.T_Producers.Find(id);
            if (t_producers == null)
            {
                return HttpNotFound();
            }
            return View(t_producers);
        }

        //
        // GET: /Producers/Create

        public ActionResult Create()
        {
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName");
            return View();
        }

        //
        // POST: /Producers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_Producers t_producers)
        {
            if (ModelState.IsValid)
            {
                db.T_Producers.Add(t_producers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", 1);
            return View(t_producers);
        }

        //
        // GET: /Producers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            T_Producers t_producers = db.T_Producers.Find(id);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_producers.StateID);
           
            if (t_producers == null)
            {
                return HttpNotFound();
            }

            return View(t_producers);
        }

        //
        // POST: /Producers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_Producers t_producers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_producers).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_producers.StateID);
           
            return View(t_producers);
        }

        //
        // GET: /Producers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            T_Producers t_producers = db.T_Producers.Find(id);
            if (t_producers == null)
            {
                return HttpNotFound();
            }
            return View(t_producers);
        }

        //
        // POST: /Producers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Producers t_producers = db.T_Producers.Find(id);
            db.T_Producers.Remove(t_producers);
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