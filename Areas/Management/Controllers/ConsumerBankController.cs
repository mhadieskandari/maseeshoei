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
    public class ConsumerBankController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/ConsumerBank/

        public ActionResult Index()
        {
            return View(db.T_Consumer_Bank.ToList());
        }

        //
        // GET: /Management/ConsumerBank/Details/5

        public ActionResult Details(short id = 0)
        {
            T_Consumer_Bank t_consumer_bank = db.T_Consumer_Bank.Find(id);
            if (t_consumer_bank == null)
            {
                return HttpNotFound();
            }
            return View(t_consumer_bank);
        }

        //
        // GET: /Management/ConsumerBank/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Management/ConsumerBank/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_Consumer_Bank t_consumer_bank)
        {
            if (ModelState.IsValid)
            {
                db.T_Consumer_Bank.Add(t_consumer_bank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_consumer_bank);
        }

        //
        // GET: /Management/ConsumerBank/Edit/5

        public ActionResult Edit(short id = 0)
        {
            T_Consumer_Bank t_consumer_bank = db.T_Consumer_Bank.Find(id);
            if (t_consumer_bank == null)
            {
                return HttpNotFound();
            }
            return View(t_consumer_bank);
        }

        //
        // POST: /Management/ConsumerBank/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_Consumer_Bank t_consumer_bank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_consumer_bank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_consumer_bank);
        }

        //
        // GET: /Management/ConsumerBank/Delete/5

        public ActionResult Delete(short id = 0)
        {
            T_Consumer_Bank t_consumer_bank = db.T_Consumer_Bank.Find(id);
            if (t_consumer_bank == null)
            {
                return HttpNotFound();
            }
            return View(t_consumer_bank);
        }

        //
        // POST: /Management/ConsumerBank/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            T_Consumer_Bank t_consumer_bank = db.T_Consumer_Bank.Find(id);
            db.T_Consumer_Bank.Remove(t_consumer_bank);
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