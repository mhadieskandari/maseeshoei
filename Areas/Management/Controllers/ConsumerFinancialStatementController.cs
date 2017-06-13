using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaseShooei.Areas.Management.Models;
using System.Dynamic;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Dictionary;
using System.Drawing;




namespace MaaseShooei.Areas.Management.Controllers
{
    [Authorize(Roles = "HighUser,Administrator,Consumer")]
    public class ConsumerFinancialStatementController : Controller
    {
        private MaaseDBEntities db = new MaaseDBEntities();

        //
        // GET: /Management/ConsumerFinancialStatement/
        [Authorize(Roles = "HighUser,Administrator,Consumer")]
        public ActionResult Index(FormCollection frm)
        {
            List<T_ConsumerFinancialStatements> t_consumerfinancialstatements;

            if (frm.Count == 0 || (frm["FromDate"].ToString().Equals("") && frm["ToDate"].ToString().Equals("") && frm["CFSID"].ToString().Equals("")))
            {
                t_consumerfinancialstatements = db.T_ConsumerFinancialStatements.Take(25).OrderByDescending(m=>m.ConsumerFinancialStatementID).ToList();

                short? typeid = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].TypeID;
                if (typeid == 3)
                {
                    int? ConsumerID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                    t_consumerfinancialstatements = t_consumerfinancialstatements.Where(m => m.ConsumerID == ConsumerID).ToList();
                }


            }
            else
            {

                String Fromdate = "";
                String ToDate = "";
                long CFSID = 0;
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
                if (!frm["CFSID"].ToString().Equals(""))
                {
                    CFSID = long.Parse(frm["CFSID"].ToString());
                    ViewBag.CFSID = CFSID;
                }


                List<T_ConsumerFinancialStatements> cfstsatement = db.T_ConsumerFinancialStatements.OrderBy(m => m.FromDate.Substring(0, 10)).ToList();
                if (!Fromdate.Equals(""))
                {
                    cfstsatement = cfstsatement.Where(m => m.FromDate.Substring(0, 10).CompareTo(Fromdate) >= 0).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

                }
                if (!ToDate.Equals(""))
                {
                    cfstsatement = cfstsatement.Where(m => m.ToDate.Substring(0, 10).CompareTo(ToDate) <= 0).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

                }
                if (CFSID != 0)
                {
                    cfstsatement = cfstsatement.Where(m => m.ConsumerFinancialStatementID == CFSID).OrderBy(m => m.FromDate.Substring(0, 10)).ToList();

                }

                //t_burdeninformations = burdens.OrderByDescending(m => m.BurdenInformationID).OrderByDescending(m => m.UnLoadDateTime.Substring(0, 10)).ToList();
                t_consumerfinancialstatements = cfstsatement.OrderByDescending(m => m.ConsumerFinancialStatementID).ToList();


                short? typeid = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].TypeID;
                if (typeid == 3)
                {
                    int? ConsumerID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                    t_consumerfinancialstatements = t_consumerfinancialstatements.Where(m => m.ConsumerID == ConsumerID).ToList();
                }

            }








            
            return View(t_consumerfinancialstatements.ToList());
        }

        //
        // GET: /Management/ConsumerFinancialStatement/Details/5
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Details(long id = 0)
        {
            T_ConsumerFinancialStatements t_consumerfinancialstatements = db.T_ConsumerFinancialStatements.Find(id);
            if (t_consumerfinancialstatements == null)
            {
                return HttpNotFound();
            }
            ViewBag.Burdens = db.T_BurdenInformations.Where(m => m.CFSID == id);
            ViewBag.Pays = db.T_ConsumersPays.Where(m => m.CFSID == id);

            
            return View(t_consumerfinancialstatements);
        }

        //
        // GET: /Management/ConsumerFinancialStatement/Create
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Create()
        {
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m=>m.StateID==true), "ConsumerID", "CompanyName");
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName");
            return View();
        }

        //
        // POST: /Management/ConsumerFinancialStatement/Create
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_ConsumerFinancialStatements t_consumerfinancialstatements)
        {
            if (ModelState.IsValid)
            {
                db.T_ConsumerFinancialStatements.Add(t_consumerfinancialstatements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true), "ConsumerID", "CompanyName", t_consumerfinancialstatements.ConsumerID);
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_consumerfinancialstatements.StateID);
            return View(t_consumerfinancialstatements);
        }

        //
        // GET: /Management/ConsumerFinancialStatement/Edit/5
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Edit(long id = 0)
        {
            T_ConsumerFinancialStatements t_consumerfinancialstatements = db.T_ConsumerFinancialStatements.Find(id);
            if (t_consumerfinancialstatements == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_consumerfinancialstatements.ConsumerID);
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_consumerfinancialstatements.StateID);
            return View(t_consumerfinancialstatements);
        }

        //
        // POST: /Management/ConsumerFinancialStatement/Edit/5
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_ConsumerFinancialStatements t_consumerfinancialstatements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_consumerfinancialstatements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsumerID = new SelectList(db.T_Consumers, "ConsumerID", "CompanyName", t_consumerfinancialstatements.ConsumerID);
            ViewBag.StateID = new SelectList(db.T_FinancialStates, "StateID", "StateName", t_consumerfinancialstatements.StateID);
            return View(t_consumerfinancialstatements);
        }



        //
        // GET: /Management/ConsumerFinancialStatement/Delete/5
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult Delete(long id = 0)
        {
            T_ConsumerFinancialStatements t_consumerfinancialstatements = db.T_ConsumerFinancialStatements.Find(id);
            if (t_consumerfinancialstatements == null)
            {
                return HttpNotFound();
            }
            return View(t_consumerfinancialstatements);
        }

        //
        // POST: /Management/ConsumerFinancialStatement/Delete/5
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ViewBag.err = "";
            try { 
                T_ConsumerFinancialStatements t_consumerfinancialstatements = db.T_ConsumerFinancialStatements.Find(id);
                db.T_ConsumerFinancialStatements.Remove(t_consumerfinancialstatements);
                db.SaveChanges();
                }
            catch
            {
                ViewBag.err = "این صورت حساب حواله دارد ابتدا حواله ها را حذف کرده سپس تلاش کنید";
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult DeleteAllBurden(long id = 0)
        {
            List<T_BurdenInformations> t_burden = db.T_BurdenInformations.Where(m => m.CFSID == id).ToList();
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
                List<T_BurdenInformations> t_burden = db.T_BurdenInformations.Where(m => m.CFSID == id).ToList();
                for (int i = 0; i < t_burden.Count; i++)
                {
                    t_burden[i].CFSID = null;
                    db.Entry(t_burden[i]).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
            }
            catch
            {
                ViewBag.err = "خطا در حذف حواله ها";
            }
            return RedirectToAction("Details", new { id=id});
        }





        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult SetBurdenForFinancial(long id)
        {
            if (id > 0)
            {
                T_ConsumerFinancialStatements tcfs = db.T_ConsumerFinancialStatements.Find(id);
                int ConsumerID = tcfs.ConsumerID;
                string FromDate = tcfs.FromDate.Substring(0, 10);
                string ToDate = tcfs.ToDate.Substring(0, 10);
                var burdeninformations = db.T_BurdenInformations.Where(m => m.T_TransportPrices.ConsumerID == ConsumerID && m.CFSID == null && m.UnLoadDateTime.Substring(0, 10).CompareTo(FromDate) >= 0 && m.UnLoadDateTime.Substring(0, 10).CompareTo(ToDate) <= 0).ToList();
            
                foreach (var item in burdeninformations)
                {
                    item.CFSID = id;
                }

               // db.T_BurdenInformations.(burdeninformations).State = EntityState.Modified;
                db.SaveChanges();



                return RedirectToAction("Details", new { id=id});
            }

            return View();
            
        }
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult DeleteFormFinancial(long id)
        {
            long? cfsid;
            if (id > 0)
            {
                var burdeninformations = db.T_BurdenInformations.Find(id);
                cfsid = burdeninformations.CFSID;
                burdeninformations.CFSID = null;
                

               // db.T_BurdenInformations.(burdeninformations).State = EntityState.Modified;
                db.SaveChanges();





                return RedirectToAction("Details", new { id = cfsid });
            }

            return View();
            
        }
        //
        // POST: /Management/ConsumerFinancialStatement/Edit/5


        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult InsertPay(long id)
        {
            //ViewBag.CFSID = id;
            ViewBag.TotalPrice = db.T_ConsumerFinancialStatements.Find(id)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ConsumerFinancialStatements.Find(id)._Payed;
            ViewBag.Creditor = db.T_ConsumerFinancialStatements.Find(id)._Creditor;
            ViewBag.AgentID = new SelectList(db.T_RecoveryAgent, "AgentID", "AgentName");
            
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName");
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName");
            ViewBag.BankID = new SelectList(db.T_Consumer_Bank, "BankId", "BankName");

            T_ConsumersPays t_cons = new T_ConsumersPays();
            t_cons.CFSID = id;
            //ViewBag.cid = id;
            
            return View(t_cons);
        }

        //
        // POST: /Management/ConsumerPay/Create
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertPay(T_ConsumersPays t_consumerspays)
        {
            if (ModelState.IsValid)
            {
               
                db.T_ConsumersPays.Add(t_consumerspays);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = t_consumerspays._CFSID });
            }

            
            ViewBag.TotalPrice = db.T_ConsumerFinancialStatements.Find(t_consumerspays._CFSID)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ConsumerFinancialStatements.Find(t_consumerspays._CFSID)._Payed;
            ViewBag.Creditor = db.T_ConsumerFinancialStatements.Find(t_consumerspays._CFSID)._Creditor;
            ViewBag.AgentID = new SelectList(db.T_RecoveryAgent, "AgentID", "AgentName");
            
            ViewBag.BankID = new SelectList(db.T_Consumer_Bank, "BankId", "BankName");
            
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_consumerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_consumerspays.PayTypeID);
            return View(t_consumerspays);
        }

        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult DeletePayConfirmed(long id)
        {
            ViewBag.err = "";

            try{
                T_ConsumersPays t_consumerspays = db.T_ConsumersPays.Find(id);
                long? cfsid=t_consumerspays._CFSID;
                db.T_ConsumersPays.Remove(t_consumerspays);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = cfsid });
            }
            catch
            {
                ViewBag.err = "این صورت حساب حواله دارد ابتدا حواله ها را حذف کرده سپس تلاش کنید";
                return RedirectToAction("Details");
            }
            
        }

        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult EditPay(long id = 0)
        {
            T_ConsumersPays t_consumerspays = db.T_ConsumersPays.Find(id);
            if (t_consumerspays == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.TotalPrice = db.T_ConsumerFinancialStatements.Find(t_consumerspays.CFSID)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ConsumerFinancialStatements.Find(t_consumerspays.CFSID)._Payed;
            ViewBag.Creditor = db.T_ConsumerFinancialStatements.Find(t_consumerspays.CFSID)._Creditor;
            ViewBag.AgentID = new SelectList(db.T_RecoveryAgent, "AgentID", "AgentName",t_consumerspays.AgentID);
            
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_consumerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_consumerspays.PayTypeID);
            ViewBag.BankID = new SelectList(db.T_Consumer_Bank, "BankId", "BankName", t_consumerspays.BankId);
            
            return View(t_consumerspays);
        }

        //
        // POST: /Management/ConsumerPay/Edit/5
        [Authorize(Roles = "HighUser,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPay(T_ConsumersPays t_consumerspays)
        {
            if (ModelState.IsValid)
            {
                long? cid = t_consumerspays._CFSID;
                db.Entry(t_consumerspays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id=cid });
            }
            ViewBag.CFSID = t_consumerspays.CFSID;

            ViewBag.AgentID = new SelectList(db.T_RecoveryAgent, "AgentID", "AgentName", t_consumerspays.AgentID);
            
            ViewBag.TotalPrice = db.T_ConsumerFinancialStatements.Find(t_consumerspays.CFSID)._BurdonsSumPrices;
            ViewBag.Payed = db.T_ConsumerFinancialStatements.Find(t_consumerspays.CFSID)._Payed;
            ViewBag.Creditor = db.T_ConsumerFinancialStatements.Find(t_consumerspays.CFSID)._Creditor;
            ViewBag.PayStateID = new SelectList(db.T_PayState, "PayStateID", "PayStateName", t_consumerspays.PayStateID);
            ViewBag.PayTypeID = new SelectList(db.T_PayType, "PayTypeID", "PayTypeName", t_consumerspays.PayTypeID);
            ViewBag.BankID = new SelectList(db.T_Consumer_Bank, "BankId", "BankName", t_consumerspays.BankId);
            
            return View(t_consumerspays);
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
                string path = Server.MapPath("~/Reports/cfs.mrt");
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
                report["CFSID"] = " where ConsumerFinancialStatementID ="+id.ToString();
                
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
                string path = Server.MapPath("~/Reports/cfs-factor.mrt");
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
                report["CFSID"] = " where ConsumerFinancialStatementID =" + id.ToString();

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
            
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName");
            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportSearch(T_ConsumerFinancialStatements t_confinans, FormCollection frm)
        {
            if (frm["ConsumerID"].ToString() != ""|| frm["FromDate"].ToString() != "" || frm["ToDate"].ToString() != "")
            {
                
               

                long consumerID = 0;               

                string fromDate = "", toDate = "";
                
                try
                {
                    consumerID = long.Parse(frm["ConsumerID"].ToString());
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



                return RedirectToAction("ShowSearch", "ConsumerFinancialStatement", new { ConsumerID = consumerID, FromDate = fromDate, ToDate = toDate });
            }
            else
            {
               
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName");
                
                return View();


            }






            // return View();
        }





        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult GetReportSnapshotSearch( long ConsumerID, string FromDate, string ToDate)
        {
            if (ConsumerID != 0 || FromDate != null || ToDate != null)
            {
                StiReport report = new StiReport();

                string path = Server.MapPath("~/Reports/cfsSearch.mrt");
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
                
                
                if (ConsumerID != 0)
                {
                    where += " where  ConsumerID =" + ConsumerID.ToString();
                }               

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (FromDate,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (ToDate,1,10) <='" + ToDate + "'";
                }
                report["CFSID"] = where;
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
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReportCheques(FormCollection frm)
        {




            long PayStateID = 0;
            long PayTypeID = 0;
            long ConsumerID = 0;
            string fromDate = "", toDate = "";

            try
            {
                PayStateID = long.Parse(frm["PayStateID"].ToString());
            }
            catch
            {

            }

            try
            {
                ConsumerID = long.Parse(frm["ConsumerID"].ToString());
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



            return RedirectToAction("ShowCheques", "ConsumerFinancialStatement", new { ConsumerID=ConsumerID,PayTypeID = PayTypeID, PayStateID = PayStateID, FromDate = fromDate, ToDate = toDate });








            // return View();
        }

        [Authorize(Roles = "HighUser,Administrator,LowUser,User")]
        public ActionResult GetReportSnapshotCheques(long ConsumerID, int PayStateID, int PayTypeID, string FromDate, string ToDate)
        {

            StiReport report = new StiReport();

            string path = Server.MapPath("~/Reports/cfs-pay-cheque.mrt");
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
            if (ConsumerID != 0)
            {
                where += " and   ConsumerID =" + ConsumerID.ToString();
            }

            if (PayStateID != 0)
            {
                where += " and   PayStateID =" + PayStateID.ToString();
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

                T_ConsumerFinancialStatements cfs = db.T_ConsumerFinancialStatements.Find(id);


                StiReport report = new StiReport();
                string path = Server.MapPath("~/Reports/cfs-pay.mrt");
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
                report["CFSID"] = " where ConsumerFinancialStatementID =" + id.ToString();
                report["kol"] = decimal.Parse(cfs._BurdonsSumPrices.ToString());
                report["albaghi"] = decimal.Parse(cfs._Creditor.ToString());
                


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