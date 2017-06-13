using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaseShooei.Areas.Management.Models;
using Stimulsoft.Report.Mvc;
using System.Drawing;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;

namespace MaaseShooei.Areas.Management.Controllers
{
     [Authorize(Roles = "HighUser,Administrator,Truck")]
    public class TruckFinancialStatementController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/TruckFinancialStatement/
         [Authorize(Roles = "HighUser,Administrator,Truck")]
        public ActionResult Index(FormCollection frm)
        {

            List<T_TruckFinancialStatements> t_truckfinancialstatements;

            if (frm.Count == 0 || (frm["FromDate"].ToString().Equals("") && frm["ToDate"].ToString().Equals("") && frm["TFSID"].ToString().Equals("")))
            {
                t_truckfinancialstatements = db.T_TruckFinancialStatements.Take(25).OrderByDescending(t => t.TruckFinancialStatementID).ToList();

                short? typeid = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].TypeID;
                if (typeid == 2)
                {
                    int? TruckID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                    t_truckfinancialstatements = t_truckfinancialstatements.Where(m => m.TruckID == TruckID).ToList();
                }


            }
            else
            {

                String Fromdate = "";
                String ToDate = "";
                long TFSID = 0;
                if (!frm["FromDate"].ToString().Equals(""))
                {
                    Fromdate = frm["FromDate"].ToString().Substring(0, 10);
                    ViewBag.fromdate = Fromdate;
                }
                //DateTime from1 = PersianDateTime.Parse(Fromdate).ToDateTime();
                if (!frm["ToDate"].ToString().Equals(""))
                {
                    ToDate = frm["ToDate"].ToString().Substring(0, 10);
                    ViewBag.todate = ToDate;
                }
                if (!frm["TFSID"].ToString().Equals(""))
                {
                    TFSID = long.Parse(frm["TFSID"].ToString());
                    ViewBag.TFSID = TFSID;
                }


                List<T_TruckFinancialStatements> Tfstsatement = db.T_TruckFinancialStatements.OrderBy(m => m.FromDate.Substring(0, 10)).ToList();
               if (!Fromdate.Equals(""))
               {
                   Tfstsatement = Tfstsatement.Where(m => m.FromDate.Substring(0, 10).CompareTo(Fromdate) >= 0).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();
               
               }
               if (!ToDate.Equals(""))
               {
                   Tfstsatement = Tfstsatement.Where(m => m.ToDate.Substring(0, 10).CompareTo(ToDate) <= 0).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

               }
               if (TFSID!=0)
               {
                   Tfstsatement = Tfstsatement.Where(m => m.TruckFinancialStatementID == TFSID).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

               }
               
               //t_burdeninformations = burdens.OrderByDescending(m => m.BurdenInformationID).OrderByDescending(m => m.UnLoadDateTime.Substring(0, 10)).ToList();
               t_truckfinancialstatements = Tfstsatement.OrderByDescending(m => m.TruckFinancialStatementID).ToList();


               short? typeid = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].TypeID;
               if (typeid == 2)
               {
                   int? TruckID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                   t_truckfinancialstatements = t_truckfinancialstatements.Where(m => m.TruckID == TruckID).ToList();
               }
            }






            



            
            
           
           
            return View(t_truckfinancialstatements);
        }

          [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult DeleteAllBurden(long id = 0)
        {
            List<T_BurdenInformations> t_burden = db.T_BurdenInformations.Where(m => m.TFSID == id).ToList();
            //if (t_burden.Count == 0)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.Burdens = t_burden;
            return View();
        }

        //
        // POST: /Management/ConsumerFinancialStatement/Delete/5
         [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost, ActionName("DeleteAllBurden")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAllBurdenConfirmed(long id)
        {
            ViewBag.err = "";
            try
            {
                List<T_BurdenInformations> t_burden = db.T_BurdenInformations.Where(m => m.TFSID == id).ToList();
                for (int i = 0; i < t_burden.Count; i++)
                {
                    t_burden[i].TFSID = null;
                    db.Entry(t_burden[i]).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch
            {
                ViewBag.err = "خطا در حذف حواله ها";
            }
            return RedirectToAction("Details", new { id = id });
        }

        //
        // GET: /Management/TruckFinancialStatement/Details/5
         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Details(long id = 0)
        {
            T_TruckFinancialStatements t_truckfinancialstatements = db.T_TruckFinancialStatements.Find(id);
            if (t_truckfinancialstatements == null)
            {
                return HttpNotFound();
            }
            ViewBag.Burdens = db.T_BurdenInformations.Where(m => m.TFSID == id);
            ViewBag.Pays = db.T_TrucksPays.Where(m => m.TFSID == id);

            return View(t_truckfinancialstatements);
        }

        //
        // GET: /Management/TruckFinancialStatement/Create
         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Create()
        {
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName");
            ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m=>m.StateID==true), "TruckID", "_NumberName");
            return View();
        }

        //
        // POST: /Management/TruckFinancialStatement/Create
         [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_TruckFinancialStatements t_truckfinancialstatements)
        {
            if (ModelState.IsValid)
            {
                db.T_TruckFinancialStatements.Add(t_truckfinancialstatements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_truckfinancialstatements.StateID);
            ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true), "TruckID", "_NumberName", t_truckfinancialstatements.TruckID);
            return View(t_truckfinancialstatements);
        }

        //
        // GET: /Management/TruckFinancialStatement/Edit/5
         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Edit(long id = 0)
        {
            T_TruckFinancialStatements t_truckfinancialstatements = db.T_TruckFinancialStatements.Find(id);
            if (t_truckfinancialstatements == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_truckfinancialstatements.StateID);
            ViewBag.TruckID = new SelectList(db.T_Trucks, "TruckID", "_NumberName", t_truckfinancialstatements.TruckID);
            return View(t_truckfinancialstatements);
        }

        //
        // POST: /Management/TruckFinancialStatement/Edit/5
         [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_TruckFinancialStatements t_truckfinancialstatements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_truckfinancialstatements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_truckfinancialstatements.StateID);
            ViewBag.TruckID = new SelectList(db.T_Trucks, "TruckID", "_NumberName", t_truckfinancialstatements.TruckID);
            return View(t_truckfinancialstatements);
        }

        //
        // GET: /Management/TruckFinancialStatement/Delete/5
         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Delete(long id = 0)
        {
            T_TruckFinancialStatements t_truckfinancialstatements = db.T_TruckFinancialStatements.Find(id);
            if (t_truckfinancialstatements == null)
            {
                return HttpNotFound();
            }
            return View(t_truckfinancialstatements);
        }

        //
        // POST: /Management/TruckFinancialStatement/Delete/5
         [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_TruckFinancialStatements t_truckfinancialstatements = db.T_TruckFinancialStatements.Find(id);
            db.T_TruckFinancialStatements.Remove(t_truckfinancialstatements);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult SetBurdenForFinancial(long id)
        {
            if (id > 0)
            {

                T_TruckFinancialStatements tcfs = db.T_TruckFinancialStatements.Find(id);
                int TruckID = tcfs.TruckID;
                string FromDate = tcfs.FromDate.Substring(0, 10);
                string ToDate = tcfs.ToDate.Substring(0, 10);

                //int ProducerID = db.T_ProducerFinancialStatements.Where(m => m.ProducerFinancialStatementID == id).ToList()[0].ProducerID;
                var burdeninformations = db.T_BurdenInformations.Where(m => m.TruckID == TruckID && m.TFSID == null && m.UnLoadDateTime.Substring(0, 10).CompareTo(FromDate) >= 0 && m.UnLoadDateTime.Substring(0, 10).CompareTo(ToDate) <= 0).ToList();



                //int TruckID = db.T_TruckFinancialStatements.Where(m => m.TruckFinancialStatementID == id).ToList()[0].TruckID;
                //var burdeninformations = db.T_BurdenInformations.Where(m => m.TruckID == TruckID && m.TFSID == null).ToList();

                foreach (var item in burdeninformations)
                {
                    item.TFSID = id;
                }

                // db.T_BurdenInformations.(burdeninformations).State = EntityState.Modified;
                db.SaveChanges();



                return RedirectToAction("Details", new { id = id });
            }

            return View();

        }

         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult DeleteFormFinancial(long id)
        {
            long? tfsid;
            if (id > 0)
            {
                var burdeninformations = db.T_BurdenInformations.Find(id);
                tfsid = burdeninformations.TFSID;
                burdeninformations.TFSID = null;


                // db.T_BurdenInformations.(burdeninformations).State = EntityState.Modified;
                db.SaveChanges();





                return RedirectToAction("Details", new { id = tfsid });
            }

            return View();

        }
         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult InsertPay(long id)
        {

            ViewBag.TotalPrice = Double.Parse(db.T_TruckFinancialStatements.Find(id)._BurdonsSumPrices.ToString());

            ViewBag.Payed = Double.Parse(db.T_TruckFinancialStatements.Find(id)._Payed.ToString());
            ViewBag.Creditor = Double.Parse(db.T_TruckFinancialStatements.Find(id)._Creditor.ToString());
            

            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber");
            ViewBag.TitleID = new SelectList(db.T_Truck_Pay_Title, "TitleID", "TitleName");

            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName");
            T_TrucksPays t_trk = new T_TrucksPays();
            t_trk.TFSID = id;

            return View(t_trk);
        }

        //
        // POST: /Management/ConsumerPay/Create
         [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertPay(T_TrucksPays t_truckspays)
        {
            if (ModelState.IsValid)
            {
                db.T_TrucksPays.Add(t_truckspays);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = t_truckspays.TFSID });
            }

            ViewBag.TotalPrice = Double.Parse(db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._BurdonsSumPrices.ToString());

            ViewBag.Payed = Double.Parse(db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._Payed.ToString());
            ViewBag.Creditor = Double.Parse(db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._Creditor.ToString());
            
             ViewBag.TitleID = new SelectList(db.T_Truck_Pay_Title, "TitleID", "TitleName");

            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber");

             ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_truckspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_truckspays.PayTypeID);
            return View(t_truckspays);
        }

         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult DeletePayConfirmed(long id)
        {
            T_TrucksPays t_truckspays = db.T_TrucksPays.Find(id);
            long? tfsid = t_truckspays.TFSID;
            db.T_TrucksPays.Remove(t_truckspays);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = tfsid });
        }

         [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult EditPay(long id = 0)
        {
            T_TrucksPays t_truckspays = db.T_TrucksPays.Find(id);
            if (t_truckspays == null)
            {
                return HttpNotFound();
            }


            //ViewBag.TotalPrice = db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._BurdonsSumPrices;
            ViewBag.TotalPrice = Double.Parse(db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._BurdonsSumPrices.ToString());
            
            ViewBag.Payed =Double.Parse( db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._Payed.ToString());
            ViewBag.Creditor =Double.Parse( db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._Creditor.ToString());
            ViewBag.TitleID = new SelectList(db.T_Truck_Pay_Title, "TitleID", "TitleName",t_truckspays.TitleID);

            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber", t_truckspays.BankId);

            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_truckspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_truckspays.PayTypeID);

            return View(t_truckspays);
        }

        //
        // POST: /Management/ConsumerPay/Edit/5
         [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPay(T_TrucksPays t_truckspays)
        {
            if (ModelState.IsValid)
            {
                long tid = t_truckspays.TFSID;
                db.Entry(t_truckspays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = tid });
            }

            ViewBag.TotalPrice = Double.Parse(db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._BurdonsSumPrices.ToString());

            ViewBag.Payed = Double.Parse(db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._Payed.ToString());
            ViewBag.Creditor = Double.Parse(db.T_TruckFinancialStatements.Find(t_truckspays.TFSID)._Creditor.ToString());
            
            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber", t_truckspays.BankId);

            ViewBag.TitleID = new SelectList(db.T_Truck_Pay_Title, "TitleID", "TitleName", t_truckspays.TitleID);

            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_truckspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_truckspays.PayTypeID);
            return View(t_truckspays);
        }


        public ActionResult ShowReport()
        {



            return View();
        }

        public ActionResult GetReportSnapshot(long id = 0)
        {
            if (id != 0)
            {
                StiReport report = new StiReport();
                string path = Server.MapPath("~/Reports/tfs.mrt");
                report.Load(path);

                report.Dictionary.Databases.Clear();


                string con = System.Configuration.ConfigurationManager.ConnectionStrings[Session["ReportConnection"].ToString()].ConnectionString;

                report.Dictionary.Databases.Add(new StiSqlDatabase("Maase", con));

                //string con = System.Configuration.ConfigurationManager.ConnectionStrings["MaaseDBEntities"].ConnectionString;
                //report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Maase", con));

                Image img = Image.FromFile(Server.MapPath(@"~/Images/logomaster.png"));
                report.Compile();
                string p = Server.MapPath(@"~/TFont");
                report["fontPath"] = p;
                report["logo"] = img;
                report["TFSID"] = " where TruckFinancialStatementID =" + id.ToString();

                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }

        public ActionResult ShowFactor()
        {



            return View();
        }

        public ActionResult GetReportSnapshot1(long id = 0)
        {
            if (id != 0)
            {
                StiReport report = new StiReport();
                string path = Server.MapPath("~/Reports/tfs-factor.mrt");
                report.Load(path);

                report.Dictionary.Databases.Clear();


                string con = System.Configuration.ConfigurationManager.ConnectionStrings[Session["ReportConnection"].ToString()].ConnectionString;

                report.Dictionary.Databases.Add(new StiSqlDatabase("Maase", con));

                //string con = System.Configuration.ConfigurationManager.ConnectionStrings["MaaseDBEntities"].ConnectionString;
                //report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Maase", con));

                Image img = Image.FromFile(Server.MapPath(@"~/Images/logomaster.png"));
                report.Compile();
                string p = Server.MapPath(@"~/TFont");
                report["fontPath"] = p;
                report["logo"] = img;
                report["TFSID"] = " where TruckFinancialStatementID =" + id.ToString();

                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            else
            {
                return RedirectToAction("index");
            }
        }


        public ActionResult ShowAccountant()
        {



            return View();
        }

        public ActionResult GetReportSnapshotAccountant(long id = 0)
        {
            if (id != 0)
            {

                T_TruckFinancialStatements tfs = db.T_TruckFinancialStatements.Find(id);


                StiReport report = new StiReport();
                string path = Server.MapPath("~/Reports/tfs-pay.mrt");
                report.Load(path);

                report.Dictionary.Databases.Clear();


                string con = System.Configuration.ConfigurationManager.ConnectionStrings[Session["ReportConnection"].ToString()].ConnectionString;

                report.Dictionary.Databases.Add(new StiSqlDatabase("Maase", con));

                //string con = System.Configuration.ConfigurationManager.ConnectionStrings["MaaseDBEntities"].ConnectionString;
                //report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Maase", con));

                Image img = Image.FromFile(Server.MapPath(@"~/Images/logomaster.png"));
                report.Compile();
                string p = Server.MapPath(@"~/TFont");
                report["fontPath"] = p;
                report["logo"] = img;
                report["TFSID"] = " where TruckFinancialStatementID =" + id.ToString();
                report["kol"] = decimal.Parse(tfs._BurdonsSumPrices.ToString());
                report["albaghi"] = decimal.Parse(tfs._Creditor.ToString());

                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            else
            {
                return RedirectToAction("index");
            }
        }





        public ActionResult ShowSearch()
        {



            return View();
        }


        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportSearch()
        {

            ViewBag.TruckID = new MultiSelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportSearch(FormCollection frm)
        {
            //if (frm["TruckID"].ToString() != "" || frm["FromDate"].ToString() != "" || frm["ToDate"].ToString() != "")
            //{



                string truckID="";

                string fromDate = "", toDate = "";

                try
                {
                    truckID = frm["TruckID"].ToString();
                }
                catch
                {

                }

                //try
                //{
                //    truckID = long.Parse(frm["TruckID"].ToString());
                //}
                //catch
                //{

                //}


                try
                {
                    fromDate = frm["FromDate"].ToString();
                }
                catch
                {

                }
                try
                {
                    toDate = frm["ToDate"].ToString();
                }
                catch
                {

                }



                return RedirectToAction("ShowSearch", "TruckFinancialStatement", new { TruckIDS = truckID, FromDate = fromDate, ToDate = toDate });
            //}
            //else
            //{

            //    ViewBag.TruckID = new MultiSelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName");

            //    return View();


            //}






            // return View();
        }





        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult GetReportSnapshotSearch(string TruckIDS, string FromDate, string ToDate)
        {
            //if (TruckIDS != null || FromDate != null || ToDate != null)
            //{
                StiReport report = new StiReport();

                string path = Server.MapPath("~/Reports/tfsSearch.mrt");
                report.Load(path);

                report.Dictionary.Databases.Clear();


                string con = System.Configuration.ConfigurationManager.ConnectionStrings[Session["ReportConnection"].ToString()].ConnectionString;

                report.Dictionary.Databases.Add(new StiSqlDatabase("Maase", con));

                //string con = System.Configuration.ConfigurationManager.ConnectionStrings["MaaseDBEntities"].ConnectionString;
                //report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Maase", con));

                Image img = Image.FromFile(Server.MapPath(@"~/Images/logomaster.png"));
                report.Compile();
                string p = Server.MapPath(@"~/TFont");
                report["fontPath"] = p;
                report["logo"] = img;
                String where = "";


                if (TruckIDS !=null)
                {
                    where += " where  TruckID IN (" + TruckIDS+")";
                }

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (FromDate,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (ToDate,1,10) <='" + ToDate + "'";
                }
                report["TFSID"] = where;
                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            //}
            //else
            //{
            //    return RedirectToAction("index");
            //}
        }




        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportCheques()
        {

            ViewBag.PayStateID = new SelectList(db.T_PayState.ToList(), "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType.ToList(), "PayTypeID", "PayTypeName");
            ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "_NumberName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportCheques(FormCollection frm)
        {




            long PayStateID  = 0;
            long PayTypeID = 0;
            long TruckID=0;

                string fromDate = "", toDate = "";

                try
                {
                    TruckID = long.Parse(frm["TruckID"].ToString());
                }
                catch
                {

                }

                try
                {
                    PayStateID = long.Parse(frm["PayStateID"].ToString());
                }
                catch
                {

                }

                try
                {
                    PayTypeID = long.Parse(frm["PayTypeID"].ToString());
                }
                catch
                {

                }


                try
                {
                    fromDate = frm["FromDate"].ToString();
                }
                catch
                {

                }
                try
                {
                    toDate = frm["ToDate"].ToString();
                }
                catch
                {

                }



                return RedirectToAction("ShowCheques", "TruckFinancialStatement", new { TruckID = TruckID,PayTypeID = PayTypeID, PayStateID = PayStateID, FromDate = fromDate, ToDate = toDate });








            // return View();
        }

        [Authorize(Roles = "HighUser,Administrator,LowUser,User")]
        public ActionResult GetReportSnapshotCheques(long TruckID,int PayStateID, int PayTypeID, string FromDate, string ToDate)
        {
           
                StiReport report = new StiReport();

                string path = Server.MapPath("~/Reports/tfs-pay-cheque.mrt");
                report.Load(path);

                report.Dictionary.Databases.Clear();


                string con = System.Configuration.ConfigurationManager.ConnectionStrings[Session["ReportConnection"].ToString()].ConnectionString;

                report.Dictionary.Databases.Add(new StiSqlDatabase("Maase", con));

                //string con = System.Configuration.ConfigurationManager.ConnectionStrings["MaaseDBEntities"].ConnectionString;
                //report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Maase", con));

                Image img = Image.FromFile(Server.MapPath(@"~/Images/logomaster.png"));
                report.Compile();
                string p = Server.MapPath(@"~/TFont");
                report["fontPath"] = p;
                report["logo"] = img;
                String where = "";


                if (PayStateID != 0)
                {
                    where += " and   PayStateID =" + PayStateID.ToString();
                }

                if (TruckID != 0)
                {
                    where += " and   TruckID =" + TruckID.ToString();
                }

                if (PayTypeID != 0)
                {
                    where += " and  PayTypeID =" + PayTypeID.ToString();
                }

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (PayDate,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (PayDate,1,10) <='" + ToDate + "'";
                }
                report["condition"] = where;
                report["FromDate"] = FromDate;
                report["ToDate"] = ToDate;
                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            
        }


        public ActionResult ShowCheques()
        {



            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}