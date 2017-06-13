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
    public class MyBankController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/MyBank/

        public ActionResult Index()
        {
            return View(db.T_BankMyAcount.ToList());
        }

        //
        // GET: /Management/MyBank/Details/5

        public ActionResult Details(short id = 0)
        {
            T_BankMyAcount t_bankmyacount = db.T_BankMyAcount.Find(id);
            if (t_bankmyacount == null)
            {
                return HttpNotFound();
            }
            return View(t_bankmyacount);
        }

        //
        // GET: /Management/MyBank/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Management/MyBank/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_BankMyAcount t_bankmyacount)
        {
            if (ModelState.IsValid)
            {
                db.T_BankMyAcount.Add(t_bankmyacount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_bankmyacount);
        }

        //
        // GET: /Management/MyBank/Edit/5

        public ActionResult Edit(short id = 0)
        {
            T_BankMyAcount t_bankmyacount = db.T_BankMyAcount.Find(id);
            if (t_bankmyacount == null)
            {
                return HttpNotFound();
            }
            return View(t_bankmyacount);
        }

        //
        // POST: /Management/MyBank/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_BankMyAcount t_bankmyacount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_bankmyacount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_bankmyacount);
        }

        //
        // GET: /Management/MyBank/Delete/5

        public ActionResult Delete(short id = 0)
        {
            T_BankMyAcount t_bankmyacount = db.T_BankMyAcount.Find(id);
            if (t_bankmyacount == null)
            {
                return HttpNotFound();
            }
            return View(t_bankmyacount);
        }

        //
        // POST: /Management/MyBank/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            T_BankMyAcount t_bankmyacount = db.T_BankMyAcount.Find(id);
            db.T_BankMyAcount.Remove(t_bankmyacount);
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