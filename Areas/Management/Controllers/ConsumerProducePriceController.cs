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
    public class ConsumerProducePriceController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /ConsumerProducePrice/

        public ActionResult Index()
        {
            var t_consumerproduceprices = db.T_ConsumerProducePrices.Include(t => t.T_Consumers).Include(t => t.T_Produces);
            return View(t_consumerproduceprices.ToList());
        }

        public ActionResult Active(int id = 0)
        {
            T_ConsumerProducePrices t_consumerproduceprices = db.T_ConsumerProducePrices.Find(id);
            if (t_consumerproduceprices == null)
            {
                return HttpNotFound();
            }

            Nullable<int> ProduceID, ConsumerID;
            ConsumerID = t_consumerproduceprices.ConsumerID;
            ProduceID = t_consumerproduceprices.ProduceID;
            //ConsumerID = t_producerproduceprices.ConsumerID;

            List<T_ConsumerProducePrices> trlist = db.T_ConsumerProducePrices.Where(m => m.ProduceID == ProduceID && m.ConsumerID == ConsumerID).ToList();

            if (trlist.Count > 0)
            {
                foreach (var item in trlist)
                {
                    if (item._ConsumerProducePriceID == id)
                    {
                        item.StateID = true;
                    }
                    else
                    {
                        item.StateID = false;
                    }

                    db.Entry(item).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }

            }

            return RedirectToAction("Index", "ConsumerProducePrice");
        }

        //
        // GET: /ConsumerProducePrice/Details/5

        public ActionResult Details(int id = 0)
        {
            T_ConsumerProducePrices t_consumerproduceprices = db.T_ConsumerProducePrices.Find(id);
            if (t_consumerproduceprices == null)
            {
                return HttpNotFound();
            }
            return View(t_consumerproduceprices);
        }

        //
        // GET: /ConsumerProducePrice/Create

        public ActionResult Create()
        {
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName");
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName");
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName");

            return View();
        }

        //
        // POST: /ConsumerProducePrice/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_ConsumerProducePrices t_consumerproduceprices)
        {
            if (ModelState.IsValid)
            {
                db.T_ConsumerProducePrices.Add(t_consumerproduceprices);
                db.SaveChanges();

                if (t_consumerproduceprices.StateID == true)
                {
                    List<T_ConsumerProducePrices> trlist = db.T_ConsumerProducePrices.Where(m => m.ProduceID == t_consumerproduceprices.ProduceID && m.ConsumerID == t_consumerproduceprices.ConsumerID).ToList();

                    if (trlist.Count > 0)
                    {
                        foreach (var item in trlist)
                        {
                            if (item._ConsumerProducePriceID == t_consumerproduceprices._ConsumerProducePriceID)
                            {
                                item.StateID = true;
                            }
                            else
                            {
                                item.StateID = false;
                            }

                            db.Entry(item).State = System.Data.EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                }
                return RedirectToAction("Index");
            }

            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_consumerproduceprices.ConsumerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_consumerproduceprices.ProduceID);

            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_consumerproduceprices.StateID);
            return View(t_consumerproduceprices);
        }

        //
        // GET: /ConsumerProducePrice/Edit/5

        public ActionResult Edit(int id = 0)
        {
            T_ConsumerProducePrices t_consumerproduceprices = db.T_ConsumerProducePrices.Find(id);
            if (t_consumerproduceprices == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_consumerproduceprices.ConsumerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_consumerproduceprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_consumerproduceprices.StateID);
            return View(t_consumerproduceprices);
        }

        //
        // POST: /ConsumerProducePrice/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_ConsumerProducePrices t_consumerproduceprices)
        {
            if (ModelState.IsValid)
            {
                bool CanEdit = true;

                List<T_BurdenInformations> tburdens = db.T_BurdenInformations.Where(m => m.CPPID == t_consumerproduceprices.ConsumerProducePriceID).ToList();

                foreach (var item in tburdens)
                {

                    if (!item.CFSID.Equals(null))
                    {
                        if (item.T_ConsumerFinancialStatements.StateID != 3)
                        {
                            CanEdit = false;
                        }
                    }
                    if (!item.PFSID.Equals(null))
                    {
                        if (item.T_ProducerFinancialStatements.StateID != 3)
                        {
                            CanEdit = false;
                        }
                    }
                    if (!item.TFSID.Equals(null))
                    {
                        if (item.T_TruckFinancialStatements.StateID != 3)
                        {
                            CanEdit = false;
                        }
                    }
                }


                if (CanEdit)
                {

                    db.Entry(t_consumerproduceprices).State = EntityState.Modified;
                    db.SaveChanges();
                    if (t_consumerproduceprices.StateID == true)
                    {
                        List<T_ConsumerProducePrices> trlist = db.T_ConsumerProducePrices.Where(m => m.ProduceID == t_consumerproduceprices.ProduceID && m.ConsumerID == t_consumerproduceprices.ConsumerID).ToList();

                        if (trlist.Count > 0)
                        {
                            foreach (var item in trlist)
                            {
                                if (item._ConsumerProducePriceID == t_consumerproduceprices.ConsumerProducePriceID)
                                {
                                    item.StateID = true;
                                }
                                else
                                {
                                    item.StateID = false;
                                }

                                db.Entry(item).State = System.Data.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }



                        return RedirectToAction("Index");
                    }
                   
                }
                else
                {
                    ViewBag.msg = "این قیمت در صورت حساب قطعی وجود دارد و اجازه ویرایش ندارید";

                }
            }
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_consumerproduceprices.ConsumerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_consumerproduceprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_consumerproduceprices.StateID);
            return View(t_consumerproduceprices);
        }

        //
        // GET: /ConsumerProducePrice/Delete/5

        public ActionResult Delete(int id = 0)
        {
            T_ConsumerProducePrices t_consumerproduceprices = db.T_ConsumerProducePrices.Find(id);
            if (t_consumerproduceprices == null)
            {
                return HttpNotFound();
            }
            return View(t_consumerproduceprices);
        }

        //
        // POST: /ConsumerProducePrice/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_ConsumerProducePrices t_consumerproduceprices = db.T_ConsumerProducePrices.Find(id);
            if (t_consumerproduceprices.StateID == false)
            {
                db.T_ConsumerProducePrices.Remove(t_consumerproduceprices);
                db.SaveChanges();
            }
            else
            {
                ViewBag.err = "این ایتم فعال است و شما امکان حذف آن را ندارید";
                return View(t_consumerproduceprices);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}