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
    public class ProducerPayController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/ProducerPay/

        public ActionResult Index()
        {
            var t_producerspays = db.T_ProducersPays.Include(t => t.T_PayState).Include(t => t.T_PayType).Include(t => t.T_ProducerFinancialStatements);
            return View(t_producerspays.ToList());
        }

        //
        // GET: /Management/ProducerPay/Details/5

        public ActionResult Details(long id = 0)
        {
            T_ProducersPays t_producerspays = db.T_ProducersPays.Find(id);
            if (t_producerspays == null)
            {
                return HttpNotFound();
            }
            return View(t_producerspays);
        }

        //
        // GET: /Management/ProducerPay/Create

        public ActionResult Create()
        {
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName");
            ViewBag.PFSID = new SelectList(db.T_ProducerFinancialStatements, "ProducerFinancialStatementID", "RegisterDate");
            return View();
        }

        //
        // POST: /Management/ProducerPay/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_ProducersPays t_producerspays)
        {
            if (ModelState.IsValid)
            {
                db.T_ProducersPays.Add(t_producerspays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_producerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_producerspays.PayTypeID);
            ViewBag.PFSID = new SelectList(db.T_ProducerFinancialStatements, "ProducerFinancialStatementID", "RegisterDate", t_producerspays.PFSID);
            return View(t_producerspays);
        }

        //
        // GET: /Management/ProducerPay/Edit/5

        public ActionResult Edit(long id = 0)
        {
            T_ProducersPays t_producerspays = db.T_ProducersPays.Find(id);
            if (t_producerspays == null)
            {
                return HttpNotFound();
            }
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_producerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_producerspays.PayTypeID);
            ViewBag.PFSID = new SelectList(db.T_ProducerFinancialStatements, "ProducerFinancialStatementID", "RegisterDate", t_producerspays.PFSID);
            return View(t_producerspays);
        }

        //
        // POST: /Management/ProducerPay/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_ProducersPays t_producerspays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_producerspays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_producerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_producerspays.PayTypeID);
            ViewBag.PFSID = new SelectList(db.T_ProducerFinancialStatements, "ProducerFinancialStatementID", "RegisterDate", t_producerspays.PFSID);
            return View(t_producerspays);
        }

        //
        // GET: /Management/ProducerPay/Delete/5

        public ActionResult Delete(long id = 0)
        {
            T_ProducersPays t_producerspays = db.T_ProducersPays.Find(id);
            if (t_producerspays == null)
            {
                return HttpNotFound();
            }
            return View(t_producerspays);
        }

        //
        // POST: /Management/ProducerPay/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_ProducersPays t_producerspays = db.T_ProducersPays.Find(id);
            db.T_ProducersPays.Remove(t_producerspays);
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