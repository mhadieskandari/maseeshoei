using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaseShooei.Areas.Management.Models;
using System.ComponentModel.DataAnnotations;
using MaaseShooei.Areas.Management.ViewModel;

namespace MaaseShooei.Areas.Management.Controllers
{
    [Authorize(Roles = "HighUser,Administrator")]
    public class TransportPriceController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        public string getServerTime()
        {
            return DateTime.Now.ToLongTimeString();
        }

        
        public JsonResult getProducers()
        {
            //string ProducerIDS;
            //List<T_TransportPrices> listtrans = db.T_TransportPrices.Where(m=>m._StateID==true).ToList();
            //if(listtrans.Count>0){
            //    ProducerIDS=listtrans[0]._ProducerID.ToString();

            //    for(int i=1;i< listtrans.Count;i++){
            //        ProducerIDS+=","+listtrans[i]._ProducerID.ToString();
            //    }
            //}

            List<Producer> p = new List<Producer>();

            foreach (var item in db.T_Producers.ToList())
            {
                Producer pr = new Producer();
                pr.ProducerID = item.ProducerID;
                pr.CompanyName = item.CompanyName;
                p.Add(pr);
            }
           

            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getConsumers()
        {
            return Json(db.T_Consumers.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getProduces(int ConsumerID, int ProducerID)
        {
            return Json(db.T_TransportPrices.Where(m => m._ProducerID == ProducerID && m._ConsumerID == ConsumerID).ToList());
        }

        public JsonResult getTransPrices(int ConsumerID,int ProducerID,int ProduceID)
        {
            return Json(db.T_TransportPrices.Where(m=>m._ProducerID==ProducerID && m._ConsumerID==ConsumerID && m._ProduceID==ProduceID).ToList());
        }


        //
        // GET: /TransportPrices/
        //[ActionName("Index")]
        public ActionResult Index(Nullable<int> produceID, Nullable<int> producerID, Nullable<int> consumerID, Nullable<bool> stateID)
        {
           
            //SelectList p = new SelectList(db.T_Produces, "ProduceID", "ProduceName");
            //ViewBag.ProduceID = p;
            //SelectList pr = new SelectList(db.T_Producers, "ProducerID", "CompanyName");
            //ViewBag.ProducerID = pr;
            //SelectList cr = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName");
            //ViewBag.ConsumerID = cr;

            //SelectList st = new SelectList(db.T_State, "StateID", "StateName");
            //ViewBag.StateID = st;

            ////var t_transportprices = db.T_TransportPrices.Include(t => t.T_Consumers).Include(t => t.T_Producers).Include(t => t.T_Produces);
            var t_transportprices = db.T_TransportPrices.Include(t => t.T_Consumers).Include(t => t.T_Producers).Include(t => t.T_Produces);

            //if (!(produceID == null ))
            //{
            //    t_transportprices = t_transportprices.Where(m => m.ProduceID == produceID);
            //}
            //if (!(producerID == null ))
            //{
            //    t_transportprices = t_transportprices.Where(m => m.ProducerID == producerID);
            //}
            //if (!(consumerID == null ))
            //{
            //    t_transportprices = t_transportprices.Where(m => m.ConsumerID == consumerID);
            //}
            //if (!(stateID == null))
            //{
            //    if (stateID == false) {
            //        t_transportprices = t_transportprices.Where(m => m.StateID == false);
            //    }
            //    else
            //    {
            //        t_transportprices = t_transportprices.Where(m => m.StateID == true);
            //    }
            //}
            
            return View(t_transportprices.ToList());
        }

        //public ActionResult Index()
        //{

        //    SelectList p = new SelectList(db.T_Produces, "ProduceID", "ProduceName");
        //    ViewBag.ProduceID = p;
        //    SelectList pr = new SelectList(db.T_Producers, "ProducerID", "ProducerName");
        //    ViewBag.ProducerID = pr;
        //    SelectList cr = new SelectList(db.T_Consumers, "ConsumerID", "ConsumerName");
        //    ViewBag.ProduceID = cr;

        //    //var t_transportprices = db.T_TransportPrices.Include(t => t.T_Consumers).Include(t => t.T_Producers).Include(t => t.T_Produces);
        //    var t_transportprices = db.T_TransportPrices.Include(t => t.T_Consumers).Include(t => t.T_Producers).Include(t => t.T_Produces);

        //    //if (produceID != null)
        //    //{
        //    //    t_transportprices = t_transportprices.Where(m => m._ProduceID == int.Parse(produceID));
        //    //}
        //    //if (producerID != null)
        //    //{
        //    //    t_transportprices = t_transportprices.Where(m => m._ProducerID == int.Parse(producerID));
        //    //}
        //    //if (consumerID != null)
        //    //{
        //    //    t_transportprices = t_transportprices.Where(m => m._ConsumerID == int.Parse(consumerID));
        //    //}

        //    return View(t_transportprices.ToList());
        //}

        //
        // GET: /TransportPrices/Details/5

        public ActionResult Details(int id = 0)
        {
            T_TransportPrices t_transportprices = db.T_TransportPrices.Find(id);
            if (t_transportprices == null)
            {
                return HttpNotFound();
            }
            return View(t_transportprices);
        }


        public ActionResult Active(int id = 0)
        {
            T_TransportPrices t_transportprices = db.T_TransportPrices.Find(id);
            if (t_transportprices == null)
            {
                return HttpNotFound();
            }

            Nullable<int> ProduceID, ProducerID, ConsumerID;
            ProducerID = t_transportprices.ProducerID;
            ProduceID = t_transportprices.ProduceID;
            ConsumerID = t_transportprices.ConsumerID;

            List<T_TransportPrices> trlist = db.T_TransportPrices.Where(m => m.ConsumerID == ConsumerID && m.ProduceID==ProduceID && m.ProducerID==ProducerID ).ToList();

            if (trlist.Count > 0)
            {
                foreach (var item in trlist)
                {
                    if (item.TransportPriceID == id)
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








            return RedirectToAction("Index", "TransportPrice");
        }

        //
        // GET: /TransportPrices/Create

        public ActionResult Create()
        {
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName");
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName");
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName");
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName");
            
            return View();
        }

        //
        // POST: /TransportPrices/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_TransportPrices t_transportprices)
        {
            if (ModelState.IsValid)
            {
                db.T_TransportPrices.Add(t_transportprices);
                db.SaveChanges();

                if (t_transportprices.StateID == true)
                {
                    List<T_TransportPrices> trlist = db.T_TransportPrices.Where(m => m.ConsumerID == t_transportprices.ConsumerID && m.ProduceID == t_transportprices.ProduceID && m.ProducerID == t_transportprices.ProducerID).ToList();

                    if (trlist.Count > 0)
                    {
                        foreach (var item in trlist)
                        {
                            if (item.TransportPriceID == t_transportprices.TransportPriceID)
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

            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_transportprices.ConsumerID);
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_transportprices.ProducerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_transportprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName",t_transportprices.StateID);

            return View(t_transportprices);
        }

       

        //
        // GET: /TransportPrices/Edit/5

        public ActionResult Edit(int id = 0)
        {
            T_TransportPrices t_transportprices = db.T_TransportPrices.Find(id);
            if (t_transportprices == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_transportprices.ConsumerID);
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_transportprices.ProducerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_transportprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_transportprices.StateID);
            return View(t_transportprices);
        }

        //
        // POST: /TransportPrices/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_TransportPrices t_transportprices)
        {
            if (ModelState.IsValid)
            {
                bool CanEdit = true;

                List<T_BurdenInformations> tburdens=db.T_BurdenInformations.Where(m=>m.TransportPriceID==t_transportprices.TransportPriceID).ToList();

                foreach(var item in tburdens){

                    if (!item.CFSID.Equals(null))
                    {
                        if (item.T_ConsumerFinancialStatements.StateID != 3 )
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
                    db.Entry(t_transportprices).State = System.Data.EntityState.Modified;
                    db.SaveChanges();

                    if (t_transportprices.StateID == true)
                    {
                        List<T_TransportPrices> trlist = db.T_TransportPrices.Where(m => m.ConsumerID == t_transportprices.ConsumerID && m.ProduceID == t_transportprices.ProduceID && m.ProducerID == t_transportprices.ProducerID).ToList();

                        if (trlist.Count > 0)
                        {
                            foreach (var item in trlist)
                            {
                                if (item.TransportPriceID == t_transportprices.TransportPriceID)
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
                    ViewBag.msg = "این قیمت در حواله های دائم استفاده شده و قابل ویرایش نیست";
                }

                
            }
           
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_transportprices.ConsumerID);
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_transportprices.ProducerID);
            ViewBag.ProduceID = new SelectList(db.T_Produces, "ProduceID", "ProduceName", t_transportprices.ProduceID);
            ViewBag.StateID = new SelectList(db.T_State, "StateID", "StateName", t_transportprices.StateID);
            return View(t_transportprices);
        }

        //
        // GET: /TransportPrices/Delete/5

        public ActionResult Delete(int id = 0)
        {
            T_TransportPrices t_transportprices = db.T_TransportPrices.Find(id);
            if (t_transportprices == null)
            {
                return HttpNotFound();
            }
            return View(t_transportprices);
        }

        //
        // POST: /TransportPrices/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            T_TransportPrices t_transportprices = db.T_TransportPrices.Find(id);
            if (t_transportprices.StateID == false)
            {
                db.T_TransportPrices.Remove(t_transportprices);
                db.SaveChanges();
                
                //return View();
            }
            else
            {
                ViewBag.err = "این ایتم فعال است و شما امکان حذف آن را ندارید";
                return View(t_transportprices);
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