using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaseShooei.Areas.Management.Models;
using Stimulsoft.Report;
using System.Drawing;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Dictionary;

namespace MaaseShooei.Areas.Management.Controllers
{
    [Authorize(Roles = "HighUser,Administrator")]
    public class ProducerFinancialStatementController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/ProducerFinancialStatement/

        public ActionResult Index(FormCollection frm)
        {
            List<T_ProducerFinancialStatements> t_producerfinancialstatements;

            if (frm.Count == 0 || (frm["FromDate"].ToString().Equals("") && frm["ToDate"].ToString().Equals("") && frm["PFSID"].ToString().Equals("")))
            {
                t_producerfinancialstatements = db.T_ProducerFinancialStatements.Take(25).OrderByDescending(t => t.ProducerFinancialStatementID).ToList();

                //short? typeid = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].TypeID;
                //if (typeid == 2)
                //{
                //    int? TruckID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                //    t_truckfinancialstatements = t_truckfinancialstatements.Where(m => m.TruckID == TruckID).ToList();
                //}


            }
            else
            {

                String Fromdate = "";
                String ToDate = "";
                long PFSID = 0;
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
                if (!frm["PFSID"].ToString().Equals(""))
                {
                    PFSID = long.Parse(frm["PFSID"].ToString());
                    ViewBag.PFSID = PFSID;
                }


                List<T_ProducerFinancialStatements> Pfstsatement = db.T_ProducerFinancialStatements.OrderBy(m => m.FromDate.Substring(0, 10)).ToList();
                if (!Fromdate.Equals(""))
                {
                    Pfstsatement = Pfstsatement.Where(m => m.FromDate.Substring(0, 10).CompareTo(Fromdate) >= 0).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

                }
                if (!ToDate.Equals(""))
                {
                    Pfstsatement = Pfstsatement.Where(m => m.ToDate.Substring(0, 10).CompareTo(ToDate) <= 0).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

                }
                if (PFSID != 0)
                {
                    Pfstsatement = Pfstsatement.Where(m => m.ProducerFinancialStatementID == PFSID).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

                }

                //t_burdeninformations = burdens.OrderByDescending(m => m.BurdenInformationID).OrderByDescending(m => m.UnLoadDateTime.Substring(0, 10)).ToList();
                t_producerfinancialstatements = Pfstsatement.OrderByDescending(m => m.ProducerFinancialStatementID).ToList();


                //short? typeid = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].TypeID;
                //if (typeid == 2)
                //{
                //    int? TruckID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                //    t_truckfinancialstatements = t_truckfinancialstatements.Where(m => m.TruckID == TruckID).ToList();
                //}
            }














            
            return View(t_producerfinancialstatements.ToList());
        }


        public ActionResult DeleteAllBurden(long id = 0)
        {
            List<T_BurdenInformations> t_burden = db.T_BurdenInformations.Where(m => m.PFSID == id).ToList();
            //if (t_burden.Count == 0)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.Burdens = t_burden;
            return View();
        }

        //
        // POST: /Management/ConsumerFinancialStatement/Delete/5

        [HttpPost, ActionName("DeleteAllBurden")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAllBurdenConfirmed(long id)
        {
            ViewBag.err = "";
            try
            {
                List<T_BurdenInformations> t_burden = db.T_BurdenInformations.Where(m => m.PFSID == id).ToList();
                for (int i = 0; i < t_burden.Count; i++)
                {
                    t_burden[i].PFSID = null;
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
        // GET: /Management/ProducerFinancialStatement/Details/5

        public ActionResult Details(long id = 0)
        {
            T_ProducerFinancialStatements t_producerfinancialstatements = db.T_ProducerFinancialStatements.Find(id);
            if (t_producerfinancialstatements == null)
            {
                return HttpNotFound();
            }
            ViewBag.Burdens = db.T_BurdenInformations.Where(m => m.PFSID == id);
            ViewBag.Pays = db.T_ProducersPays.Where(m => m.PFSID == id);


            return View(t_producerfinancialstatements);
        }

        //
        // GET: /Management/ProducerFinancialStatement/Create

        public ActionResult Create()
        {
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName");
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m=>m.StateID==true), "ProducerID", "CompanyName");
            return View();
        }

        //
        // POST: /Management/ProducerFinancialStatement/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_ProducerFinancialStatements t_producerfinancialstatements)
        {
            if (ModelState.IsValid)
            {
                db.T_ProducerFinancialStatements.Add(t_producerfinancialstatements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_producerfinancialstatements.StateID);
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true), "ProducerID", "CompanyName", t_producerfinancialstatements.ProducerID);
            return View(t_producerfinancialstatements);
        }

        //
        // GET: /Management/ProducerFinancialStatement/Edit/5

        public ActionResult Edit(long id = 0)
        {
            T_ProducerFinancialStatements t_producerfinancialstatements = db.T_ProducerFinancialStatements.Find(id);
            if (t_producerfinancialstatements == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_producerfinancialstatements.StateID);
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_producerfinancialstatements.ProducerID);
            return View(t_producerfinancialstatements);
        }

        //
        // POST: /Management/ProducerFinancialStatement/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_ProducerFinancialStatements t_producerfinancialstatements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_producerfinancialstatements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_producerfinancialstatements.StateID);
            ViewBag.ProducerID = new SelectList(db.T_Producers, "ProducerID", "CompanyName", t_producerfinancialstatements.ProducerID);
            return View(t_producerfinancialstatements);
        }

        //
        // GET: /Management/ProducerFinancialStatement/Delete/5

        public ActionResult Delete(long id = 0)
        {
            T_ProducerFinancialStatements t_producerfinancialstatements = db.T_ProducerFinancialStatements.Find(id);
            if (t_producerfinancialstatements == null)
            {
                return HttpNotFound();
            }
            return View(t_producerfinancialstatements);
        }

        //
        // POST: /Management/ProducerFinancialStatement/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_ProducerFinancialStatements t_producerfinancialstatements = db.T_ProducerFinancialStatements.Find(id);
            db.T_ProducerFinancialStatements.Remove(t_producerfinancialstatements);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SetBurdenForFinancial(long id)
        {
            if (id > 0)
            {

                T_ProducerFinancialStatements tcfs = db.T_ProducerFinancialStatements.Find(id);
                int ProducerID = tcfs.ProducerID;
                string FromDate = tcfs.FromDate.Substring(0, 10);
                string ToDate = tcfs.ToDate.Substring(0, 10);

                //int ProducerID = db.T_ProducerFinancialStatements.Where(m => m.ProducerFinancialStatementID == id).ToList()[0].ProducerID;
                var burdeninformations = db.T_BurdenInformations.Where(m => m.T_TransportPrices.ProducerID == ProducerID && m.PFSID == null && m.UnLoadDateTime.Substring(0, 10).CompareTo(FromDate) >= 0 && m.UnLoadDateTime.Substring(0, 10).CompareTo(ToDate) <= 0).ToList();

                foreach (var item in burdeninformations)
                {
                    item.PFSID = id;
                }

                
                db.SaveChanges();

                return RedirectToAction("Details", new { id = id });
            }

            return View();

        }

        public ActionResult DeleteFormFinancial(long id)
        {
            long? pfsid;
            if (id > 0)
            {
                var burdeninformations = db.T_BurdenInformations.Find(id);
                pfsid = burdeninformations.PFSID;
                burdeninformations.PFSID = null;


                // db.T_BurdenInformations.(burdeninformations).State = EntityState.Modified;
                db.SaveChanges();





                return RedirectToAction("Details", new { id = pfsid });
            }

            return View();

        }


        public ActionResult InsertPay(long id)
        {

            ViewBag.TotalPrice = db.T_ProducerFinancialStatements.Find(id)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ProducerFinancialStatements.Find(id)._Payed;
            ViewBag.Creditor = db.T_ProducerFinancialStatements.Find(id)._Creditor;

            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber");
            ViewBag.TitleID = new SelectList(db.T_Producer_Pay_Title, "TitleID", "TitleName");

            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName");

            T_ProducersPays t_prods = new T_ProducersPays();
            t_prods.PFSID = id;

            return View(t_prods);
        }

        //
        // POST: /Management/ConsumerPay/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertPay(T_ProducersPays t_producersspays)
        {
            if (ModelState.IsValid)
            {
                db.T_ProducersPays.Add(t_producersspays);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = t_producersspays._PFSID });
            }
            ViewBag.TotalPrice = db.T_ProducerFinancialStatements.Find(t_producersspays._PFSID)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ProducerFinancialStatements.Find(t_producersspays._PFSID)._Payed;
            ViewBag.Creditor = db.T_ProducerFinancialStatements.Find(t_producersspays._PFSID)._Creditor;

            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber");

            ViewBag.TitleID = new SelectList(db.T_Producer_Pay_Title, "TitleID", "TitleName");
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName");
            return View(t_producersspays);
        }


        public ActionResult DeletePayConfirmed(long id)
        {
            T_ProducersPays t_producerspays = db.T_ProducersPays.Find(id);
            long? pfsid = t_producerspays._PFSID;
            db.T_ProducersPays.Remove(t_producerspays);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = pfsid });
        }


        public ActionResult EditPay(long id = 0)
        {
            T_ProducersPays t_producerspays = db.T_ProducersPays.Find(id);
            if (t_producerspays == null)
            {
                return HttpNotFound();
            }

            ViewBag.TotalPrice = db.T_ProducerFinancialStatements.Find(t_producerspays._PFSID)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ProducerFinancialStatements.Find(t_producerspays._PFSID)._Payed;
            ViewBag.Creditor = db.T_ProducerFinancialStatements.Find(t_producerspays._PFSID)._Creditor;

            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber", t_producerspays.BankId);

            ViewBag.TitleID = new SelectList(db.T_Producer_Pay_Title, "TitleID", "TitleName", t_producerspays.TitleID);
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_producerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_producerspays.PayTypeID);

            return View(t_producerspays);
        }

        //
        // POST: /Management/ConsumerPay/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPay(T_ProducersPays t_producerspays)
        {
            if (ModelState.IsValid)
            {
                long? pid = t_producerspays._PFSID;
                db.Entry(t_producerspays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = pid });
            }
            ViewBag.TotalPrice = db.T_ProducerFinancialStatements.Find(t_producerspays._PFSID)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ProducerFinancialStatements.Find(t_producerspays._PFSID)._Payed;
            ViewBag.Creditor = db.T_ProducerFinancialStatements.Find(t_producerspays._PFSID)._Creditor;
            ViewBag.BankID = new SelectList(db.T_BankMyAcount, "BankId", "_BankNumber", t_producerspays.BankId);

            ViewBag.TitleID = new SelectList(db.T_Producer_Pay_Title, "TitleID", "TitleName", t_producerspays.TitleID);
            
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_producerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_producerspays.PayTypeID);
            return View(t_producerspays);
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
                string path = Server.MapPath("~/Reports/pfs.mrt");
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
                report["PFSID"] = " where ProducerFinancialStatementID =" + id.ToString();

                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            else
            {
                return RedirectToAction("index");
            }
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
                string path = Server.MapPath("~/Reports/pfs-factor.mrt");
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
                report["PFSID"] = " where ProducerFinancialStatementID =" + id.ToString();

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

            ViewBag.ProducerID = new SelectList(db.T_Producers.ToList(), "ProducerID", "CompanyName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportSearch(T_ProducerFinancialStatements t_confinans, FormCollection frm)
        {
            if (frm["ProducerID"].ToString() != "" || frm["FromDate"].ToString() != "" || frm["ToDate"].ToString() != "")
            {



                long ProducerID = 0;

                string fromDate = "", toDate = "";

                try
                {
                    ProducerID = long.Parse(frm["ProducerID"].ToString());
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



                return RedirectToAction("ShowSearch", "ProducerFinancialStatement", new { ProducerID = ProducerID, FromDate = fromDate, ToDate = toDate });
            }
            else
            {

                ViewBag.ProducerID = new SelectList(db.T_Producers.ToList(), "ProducerID", "CompanyName");

                return View();


            }






            // return View();
        }





        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult GetReportSnapshotSearch(long ProducerID, string FromDate, string ToDate)
        {
            if (ProducerID != 0 || FromDate != null || ToDate != null)
            {
                StiReport report = new StiReport();

                string path = Server.MapPath("~/Reports/pfsSearch.mrt");
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


                if (ProducerID != 0)
                {
                    where += " where  ProducerID =" + ProducerID.ToString();
                }

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (FromDate,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (ToDate,1,10) <='" + ToDate + "'";
                }
                report["PFSID"] = where;
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




        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportCheques()
        {

            ViewBag.PayStateID = new SelectList(db.T_PayState.ToList(), "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType.ToList(), "PayTypeID", "PayTypeName");
            ViewBag.ProducerID = new SelectList(db.T_Producers.ToList(), "ProducerID", "CompanyName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportCheques(FormCollection frm)
        {




            long PayStateID = 0;
            long PayTypeID = 0;
            long ProducerID = 0;
            string fromDate = "", toDate = "";

            try
            {
                ProducerID = long.Parse(frm["ProducerID"].ToString());
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



            return RedirectToAction("ShowCheques", "ProducerFinancialStatement", new { ProducerID = ProducerID,PayTypeID = PayTypeID, PayStateID = PayStateID, FromDate = fromDate, ToDate = toDate });








            // return View();
        }

        [Authorize(Roles = "HighUser,Administrator,LowUser,User")]
        public ActionResult GetReportSnapshotCheques(long ProducerID,int PayStateID, int PayTypeID, string FromDate, string ToDate)
        {

            StiReport report = new StiReport();

            string path = Server.MapPath("~/Reports/pfs-pay-cheque.mrt");
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
            if (ProducerID != 0)
            {
                where += " and   ProducerID =" + ProducerID.ToString();
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


        public ActionResult ShowAccountant()
        {



            return View();
        }

        public ActionResult GetReportSnapshotAccountant(long id = 0)
        {
            if (id != 0)
            {

                T_ProducerFinancialStatements pfs = db.T_ProducerFinancialStatements.Find(id);


                StiReport report = new StiReport();
                string path = Server.MapPath("~/Reports/pfs-pay.mrt");
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
                report["PFSID"] = " where ProducerFinancialStatementID =" + id.ToString();
                report["kol"] = decimal.Parse(pfs._BurdonsSumPrices.ToString());
                report["albaghi"] = decimal.Parse(pfs._Creditor.ToString());



                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}