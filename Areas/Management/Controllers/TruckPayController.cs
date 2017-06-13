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
    public class TruckPayController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/TruckPay/

        public ActionResult Index()
        {
            var t_truckspays = db.T_TrucksPays.Include(t => t.T_PayState).Include(t => t.T_PayType).Include(t => t.T_TruckFinancialStatements);
            return View(t_truckspays.ToList());
        }

        //
        // GET: /Management/TruckPay/Details/5

        public ActionResult Details(long id = 0)
        {
            T_TrucksPays t_truckspays = db.T_TrucksPays.Find(id);
            if (t_truckspays == null)
            {
                return HttpNotFound();
            }
            return View(t_truckspays);
        }

        //
        // GET: /Management/TruckPay/Create

        public ActionResult Create()
        {
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName");
            ViewBag.TFSID = new SelectList(db.T_TruckFinancialStatements, "TruckFinancialStatementID", "RegisterDate");
            return View();
        }

        //
        // POST: /Management/TruckPay/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_TrucksPays t_truckspays)
        {
            if (ModelState.IsValid)
            {
                db.T_TrucksPays.Add(t_truckspays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_truckspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_truckspays.PayTypeID);
            ViewBag.TFSID = new SelectList(db.T_TruckFinancialStatements, "TruckFinancialStatementID", "RegisterDate", t_truckspays.TFSID);
            return View(t_truckspays);
        }

        //
        // GET: /Management/TruckPay/Edit/5

        public ActionResult Edit(long id = 0)
        {
            T_TrucksPays t_truckspays = db.T_TrucksPays.Find(id);
            if (t_truckspays == null)
            {
                return HttpNotFound();
            }
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_truckspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_truckspays.PayTypeID);
            ViewBag.TFSID = new SelectList(db.T_TruckFinancialStatements, "TruckFinancialStatementID", "RegisterDate", t_truckspays.TFSID);
            return View(t_truckspays);
        }

        //
        // POST: /Management/TruckPay/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_TrucksPays t_truckspays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_truckspays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_truckspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_truckspays.PayTypeID);
            ViewBag.TFSID = new SelectList(db.T_TruckFinancialStatements, "TruckFinancialStatementID", "RegisterDate", t_truckspays.TFSID);
            return View(t_truckspays);
        }

        //
        // GET: /Management/TruckPay/Delete/5

        public ActionResult Delete(long id = 0)
        {
            T_TrucksPays t_truckspays = db.T_TrucksPays.Find(id);
            if (t_truckspays == null)
            {
                return HttpNotFound();
            }
            return View(t_truckspays);
        }

        //
        // POST: /Management/TruckPay/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_TrucksPays t_truckspays = db.T_TrucksPays.Find(id);
            db.T_TrucksPays.Remove(t_truckspays);
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