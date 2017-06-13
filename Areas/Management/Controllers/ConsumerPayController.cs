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
    public class ConsumerPayController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/ConsumerPay/

        public ActionResult Index()
        {
            var t_consumerspays = db.T_ConsumersPays.Include(t => t.T_ConsumerFinancialStatements).Include(t => t.T_PayState).Include(t => t.T_PayType);
            return View(t_consumerspays.ToList());
        }

        //
        // GET: /Management/ConsumerPay/Details/5

        public ActionResult Details(long id = 0)
        {
            T_ConsumersPays t_consumerspays = db.T_ConsumersPays.Find(id);
            if (t_consumerspays == null)
            {
                return HttpNotFound();
            }
            return View(t_consumerspays);
        }

        //
        // GET: /Management/ConsumerPay/Create

        public ActionResult Create()
        {
            ViewBag.CFSID = new SelectList(db.T_ConsumerFinancialStatements, "ConsumerFinancialStatementID", "RegisterDate");
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName");
            return View();
        }

        //
        // POST: /Management/ConsumerPay/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_ConsumersPays t_consumerspays)
        {
            if (ModelState.IsValid)
            {
                db.T_ConsumersPays.Add(t_consumerspays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CFSID = new SelectList(db.T_ConsumerFinancialStatements, "ConsumerFinancialStatementID", "RegisterDate", t_consumerspays.CFSID);
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_consumerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_consumerspays.PayTypeID);
            return View(t_consumerspays);
        }

        //
        // GET: /Management/ConsumerPay/Edit/5

        public ActionResult Edit(long id = 0)
        {
            T_ConsumersPays t_consumerspays = db.T_ConsumersPays.Find(id);
            if (t_consumerspays == null)
            {
                return HttpNotFound();
            }
            ViewBag.CFSID = new SelectList(db.T_ConsumerFinancialStatements, "ConsumerFinancialStatementID", "RegisterDate", t_consumerspays.CFSID);
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_consumerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_consumerspays.PayTypeID);
            return View(t_consumerspays);
        }

        //
        // POST: /Management/ConsumerPay/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_ConsumersPays t_consumerspays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_consumerspays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CFSID = new SelectList(db.T_ConsumerFinancialStatements, "ConsumerFinancialStatementID", "RegisterDate", t_consumerspays.CFSID);
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_consumerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_consumerspays.PayTypeID);
            return View(t_consumerspays);
        }

        //
        // GET: /Management/ConsumerPay/Delete/5

        public ActionResult Delete(long id = 0)
        {
            T_ConsumersPays t_consumerspays = db.T_ConsumersPays.Find(id);
            if (t_consumerspays == null)
            {
                return HttpNotFound();
            }
            return View(t_consumerspays);
        }

        //
        // POST: /Management/ConsumerPay/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_ConsumersPays t_consumerspays = db.T_ConsumersPays.Find(id);
            db.T_ConsumersPays.Remove(t_consumerspays);
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