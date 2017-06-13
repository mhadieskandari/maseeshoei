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
    public class ProducerProducePriceController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /ProducerProducePrices/

        public ActionResult Index()
        {
            var t_producerproduceprices = db.T_ProducerProducePrices.Include(t => t.T_Producers).Include(t => t.T_Produces);
            return View(t_producerproduceprices.ToList());
        }

        public ActionResult Active(int id = 0)
        {
            T_ProducerProducePrices t_producerproduceprices = db.T_ProducerProducePrices.Find(id);
            if (t_producerproduceprices == null)
            {
                return HttpNotFound();
            }

            Nullable<int> ProduceID, ProducerID;
            ProducerID = t_producerproduceprices.ProducerID;
            ProduceID = t_producerproduceprices.ProduceID;
            //ConsumerID = t_producerproduceprices.ConsumerID;

            List<T_ProducerProducePrices> trlist = db.T_ProducerProducePrices.Where(m => m.ProduceID == ProduceID && m.ProducerID == ProducerID).ToList();

            if (trlist.Count > 0)
            {
                foreach (var item in trlist)
                {
                    if (item._ProducerProducePriceID == id)
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

            return RedirectToAction("Index", "ProducerProducePrice");
        }


        //
        // GET: /ProducerProducePrices/Details/5

        public ActionResult Details(int id = 0)
        {
            T_ProducerProducePrices t_producerproduceprices = db.T_ProducerProducePrices.Find(id);
            if (t_producerproduceprices == null)
            {
                return HttpNotFound();
            }
            return View(t_producerproduceprices);
        }

        //
        // GET: /ProducerProducePrices/Create

        public ActionResult Create()
        {
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName");
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName");
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName");
            return View();
        }

        //
        // POST: /ProducerProducePrices/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_ProducerProducePrices t_producerproduceprices)
        {
            if (ModelState.IsValid)
            {
                db.T_ProducerProducePrices.Add(t_producerproduceprices);
                db.SaveChanges();

                if (t_producerproduceprices.StateID == true)
                {
                    List<T_ProducerProducePrices> trlist = db.T_ProducerProducePrices.Where(m => m.ProduceID == t_producerproduceprices.ProduceID && m.ProducerID == t_producerproduceprices.ProducerID).ToList();

                    if (trlist.Count > 0)
                    {
                        foreach (var item in trlist)
                        {
                            if (item._ProducerProducePriceID == t_producerproduceprices.ProducerProducePriceID)
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

            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_producerproduceprices.ProducerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_producerproduceprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName",t_producerproduceprices.StateID);
            return View(t_producerproduceprices);
        }

        //
        // GET: /ProducerProducePrices/Edit/5

        public ActionResult Edit(int id = 0)
        {
            T_ProducerProducePrices t_producerproduceprices = db.T_ProducerProducePrices.Find(id);
            if (t_producerproduceprices == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_producerproduceprices.ProducerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_producerproduceprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_producerproduceprices.StateID);

            return View(t_producerproduceprices);
        }

        //
        // POST: /ProducerProducePrices/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_ProducerProducePrices t_producerproduceprices)
        {
            if (ModelState.IsValid)
            {
                bool CanEdit = true;

                List<T_BurdenInformations> tburdens = db.T_BurdenInformations.Where(m => m.PPPID == t_producerproduceprices.ProducerProducePriceID).ToList();

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

                    db.Entry(t_producerproduceprices).State = System.Data.EntityState.Modified;
                    db.SaveChanges();

                    if (t_producerproduceprices.StateID == true)
                    
                    {
                        List<T_ProducerProducePrices> trlist = db.T_ProducerProducePrices.Where(m => m.ProduceID == t_producerproduceprices.ProduceID && m.ProducerID == t_producerproduceprices.ProducerID).ToList();

                        if (trlist.Count > 0)
                        {
                            foreach (var item in trlist)
                            {
                                if (item._ProducerProducePriceID == t_producerproduceprices.ProducerProducePriceID)
                                {
                                    item.StateID = true;
                                }
                                else
                                {
                                    item.StateID = false;
                                }

                                db.Entry(item).State = System.Data.EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }

                    }
                    
                }
                else
                {
                    ViewBag.msg = "این قیمت در صورت حساب قطعی وجود دارد و اجازه ویرایش ندارید";

                }


               
            }
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_producerproduceprices.ProducerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_producerproduceprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_producerproduceprices.StateID);

            return View(t_producerproduceprices);
        }

        //
        // GET: /ProducerProducePrices/Delete/5

        public ActionResult Delete(int id = 0)
        {
            T_ProducerProducePrices t_producerproduceprices = db.T_ProducerProducePrices.Find(id);
            if (t_producerproduceprices == null)
            {
                return HttpNotFound();
            }
            return View(t_producerproduceprices);
        }

        //
        // POST: /ProducerProducePrices/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_ProducerProducePrices t_producerproduceprices = db.T_ProducerProducePrices.Find(id);
            if (t_producerproduceprices.StateID == false)
            {
                db.T_ProducerProducePrices.Remove(t_producerproduceprices);
                db.SaveChanges();
            }
            else
            {
                ViewBag.err="این ایتم فعال است و شما امکان حذف آن را ندارید";
                return View(t_producerproduceprices);
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