using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaseShooei.Areas.Management.Models;
using MaaseShooei.Areas.Management.ViewModel;
using System.Web.Security;
using System.Drawing;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;
using System.Globalization;
using System.Data.Objects;
using WebMatrix.WebData;

namespace MaaseShooei.Areas.Management.Controllers
{
      [Authorize(Roles = "HighUser,Administrator,User,LowUser,Truck,Consumer")]
    public class BurdenInformationController : Controller
    {
        
        private MaaseDBEntities db = new MaaseDBEntities();




        //[HttpGet]
        //public ActionResult SelectTransportPrice()
        //{
        //    var t_transportprices = db.T_TransportPrices.Where(p => p.T_State.StateID == true).ToList();           
        //    return View(t_transportprices);
        //}

        //[HttpPost]
        //public ActionResult SelectTransportPrice(T_TransportPrices transportprice)
        //{
        //    TempData["tpid"] = transportprice.TransportPriceID;
        //    return View();
        //}


        //
        // GET: /Default1/


        [Authorize(Roles = "HighUser,Administrator,User,LowUser,Truck,Consumer")]
        public ActionResult Index(FormCollection frm)
        {
            List<T_BurdenInformations> t_burdeninformations;
            //String date = PersianDateTime.Now.ToString();
            //date = date.Substring(0, 10);
            //t_burdeninformations = db.T_BurdenInformations.OrderByDescending(m => m.BurdenInformationID).ToList();

            if (frm.Count == 0 || (frm["FromDate"].ToString().Equals("") && frm["ToDate"].ToString().Equals("") && frm["BID"].ToString().Equals("")))
            {
                t_burdeninformations = db.T_BurdenInformations.Take(25).OrderByDescending(m => m.BurdenInformationID).ToList();

                //String date1 = PersianDateTime.Now.ToString();
                //date1 = date.Substring(0, 10);
                if (User.IsInRole("Truck"))
                {
                    int? TruckID = db.T_Users.Where(m=>m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                    t_burdeninformations = t_burdeninformations.Where(m => m.TruckID == int.Parse(TruckID.ToString())).OrderByDescending(m => m.BurdenInformationID).ToList();

                }
                if (User.IsInRole("Consumer"))
                {
                    int? ConsumerID = db.T_Users.Where(m=>m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                    t_burdeninformations = t_burdeninformations.Where(m => m.T_TransportPrices.ConsumerID == int.Parse(ConsumerID.ToString())).OrderByDescending(m => m.BurdenInformationID).ToList();

                }
                
            }
            else
            {
                String Fromdate="";
                String ToDate="";
                long BID = 0;
                if (!frm["FromDate"].ToString().Equals("")) { 
                     Fromdate = frm["FromDate"].ToString().Substring(0,10);
                     ViewBag.fromdate = Fromdate;
                }
                //DateTime from1 = PersianDateTime.Parse(Fromdate).ToDateTime();
                if (!frm["ToDate"].ToString().Equals(""))
                {
                    ToDate = frm["ToDate"].ToString().Substring(0, 10);
                    ViewBag.todate = ToDate;
                }
                if (!frm["BID"].ToString().Equals(""))
                {
                    BID = long.Parse(frm["BID"].ToString());
                    ViewBag.BID = BID;
                }
                
                //DateTime to = PersianDateTime.Parse(ToDate).ToDateTime();
                
                


                //int s;
                //s=Fromdate.CompareTo(ToDate);
                //System.Console.WriteLine(s);
                //t_burdeninformations = db.T_BurdenInformations.Where(m => (PersianDateTime.Parse(m.RegisterDateTime).ToDateTime().Year >= from.Year && PersianDateTime.Parse(m.RegisterDateTime).ToDateTime().Month >= from.Month && PersianDateTime.Parse(m.RegisterDateTime).ToDateTime().Day >= from.Day) && (PersianDateTime.Parse(m.RegisterDateTime).ToDateTime().Year <= to.Year && PersianDateTime.Parse(m.RegisterDateTime).ToDateTime().Month <= to.Month && PersianDateTime.Parse(m.RegisterDateTime).ToDateTime().Day <= to.Day)).OrderByDescending(m => m.BurdenInformationID).ToList();
                // var sd = db.T_BurdenInformations.Where(m => (m._ENRegisterDateTime.Year >= from.Year && m._ENRegisterDateTime.Month >= from.Month && m._ENRegisterDateTime.Day >= from.Day) && (m._ENRegisterDateTime.Year <= to.Year && m._ENRegisterDateTime.Month <= to.Month && m._ENRegisterDateTime.Day <= to.Day)).OrderByDescending(m => m.BurdenInformationID).AsQueryable();
                //t_burdeninformations = db.T_BurdenInformations.Where(m =>m.RegisterDateTime.Substring(0,10)==Fromdate.Substring(0,10)).ToList();
                //var t_b = from s in db.T_BurdenInformations
                //          join d in db.T_BurdenInformations on s.BurdenInformationID equals d.BurdenInformationID
                //          where s._ENRegisterDateTime >= from1
                //          select s;


                    //t_burdeninformations=t_b.ToList();

                List<T_BurdenInformations> burdens = db.T_BurdenInformations.OrderBy(m => m.UnLoadDateTime.Substring(0, 10)).ToList();
               if (!Fromdate.Equals(""))
               {
                   burdens = burdens.Where(m => m.UnLoadDateTime.Substring(0, 10).CompareTo(Fromdate) >= 0).OrderBy(m => m.UnLoadDateTime.Substring(0, 10)).ToList();
               
               }
               if (!ToDate.Equals(""))
               {
                   burdens = burdens.Where(m => m.UnLoadDateTime.Substring(0, 10).CompareTo(ToDate) <= 0).OrderBy(m => m.UnLoadDateTime.Substring(0, 10)).ToList();

               }
               if (BID!=0)
               {
                   burdens = burdens.Where(m => m.BurdenInformationID == BID).OrderBy(m => m.UnLoadDateTime.Substring(0, 10)).ToList();

               }
               if (User.IsInRole("Truck"))
               {
                   int? TruckID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                   burdens = burdens.Where(m => m.TruckID == int.Parse(TruckID.ToString())).OrderByDescending(m => m.BurdenInformationID).ToList();

               }
               if (User.IsInRole("Consumer"))
               {
                   int? ConsumerID = db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID;
                   burdens = burdens.Where(m => m.T_TransportPrices.ConsumerID == int.Parse(ConsumerID.ToString())).OrderByDescending(m => m.BurdenInformationID).ToList();

               }
               //t_burdeninformations = burdens.OrderByDescending(m => m.BurdenInformationID).OrderByDescending(m => m.UnLoadDateTime.Substring(0, 10)).ToList();
               t_burdeninformations = burdens.OrderByDescending(m => m.BurdenInformationID).ToList();

            }


            //@ViewBag.DBYear = Statics.dbconnection;
            return View(t_burdeninformations);
        }
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReport()
        {
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m=> m.StateID==true).ToList(), "ProducerID", "CompanyName");
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");
            ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "_NumberName");
            ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HighUser,Administrator")]
        public ActionResult ShowReport(FormCollection frm)
        {
            //if (frm["ProducerID"].ToString() != "" || frm["ProduceID"].ToString() != "" || frm["ConsumerID"].ToString() != "" || frm["TruckID"].ToString() != "" || frm["FromDate"].ToString() != "" || frm["ToDate"].ToString() != "")
            //{ 
                string producerid= "";
                string produceid = "";
                string consumerID = "";
                string truckID = "";
                string fromDate = "", toDate = "";
                try
                {
                    producerid = frm["ProducerID"].ToString();
                }
                catch
                {

                }
                try
                {
                    produceid = frm["ProduceID"].ToString();
                }
                catch
                {

                } 
                try
                {
                    consumerID = frm["ConsumerID"].ToString();
                }
                catch
                {

                }
                try
                {
                    truckID = frm["TruckID"].ToString();
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



                return RedirectToAction("Report","BurdenInformation", new { ProducerID = producerid, ConsumerID = consumerID, ProduceID = produceid, TruckID = truckID, FromDate = fromDate, ToDate = toDate });
            //}
            //else
            //{
            //    ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m=>m.StateID==true).ToList(), "ProducerID", "CompanyName");
            //    ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");
            //    ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "Number");
            //    ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");
            //    return View(); 


            //}
           





           // return View();
        }


        [Authorize(Roles = "User,LowUser")]
        public ActionResult ShowReport1()
        {
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true).ToList(), "ProducerID", "CompanyName");
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");
            ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "Number");
            ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");

            return View();
        }

       [Authorize(Roles = "User,LowUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowReport1(FormCollection frm)
        {
            if (frm["ProducerID"].ToString() != "" || frm["ProduceID"].ToString() != "" || frm["ConsumerID"].ToString() != "" || frm["TruckID"].ToString() != "" || frm["FromDate"].ToString() != "" || frm["ToDate"].ToString() != "")
            {
                long producerid = 0;
                long produceid = 0;
                long consumerID = 0;
                long truckID = 0;
                string fromDate = "", toDate = "";
                try
                {
                    producerid = long.Parse(frm["ProducerID"].ToString());
                }
                catch
                {

                }
                try
                {
                    produceid = long.Parse(frm["ProduceID"].ToString());
                }
                catch
                {

                }
                try
                {
                    consumerID = long.Parse(frm["ConsumerID"].ToString());
                }
                catch
                {

                }
                try
                {
                    truckID = long.Parse(frm["TruckID"].ToString());
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



                return RedirectToAction("Report1", "BurdenInformation", new { ProducerID = producerid, ConsumerID = consumerID, ProduceID = produceid, TruckID = truckID, FromDate = fromDate, ToDate = toDate });
            }
            else
            {
                ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true).ToList(), "ProducerID", "CompanyName");
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");
                ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "Number");
                ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");
                return View();


            }






            // return View();
        }
        
        [Authorize(Roles = "Truck")]
        public ActionResult ShowReportTruck()
        {
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true).ToList(), "ProducerID", "CompanyName");
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");
            //ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "Number");
            ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");

            return View();
        }

          [Authorize(Roles = "Truck")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowReportTruck(T_BurdenInformations t_burden, FormCollection frm)
        {
            if (frm["ProducerID"].ToString() != "" || frm["ProduceID"].ToString() != "" || frm["ConsumerID"].ToString() != "" || frm["FromDate"].ToString() != "" || frm["ToDate"].ToString() != "")
            {
                long producerid = 0;
                long produceid = 0;
                long consumerID = 0;
                long truckID = 0;
                string fromDate = "", toDate = "";
                try
                {
                    producerid = long.Parse(frm["ProducerID"].ToString());
                }
                catch
                {

                }
                try
                {
                    produceid = long.Parse(frm["ProduceID"].ToString());
                }
                catch
                {

                }
                try
                {
                    consumerID = long.Parse(frm["ConsumerID"].ToString());
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



                return RedirectToAction("ReportTruck", "BurdenInformation", new { ProducerID = producerid, ConsumerID = consumerID, ProduceID = produceid, FromDate = fromDate, ToDate = toDate });
            }
            else
            {
                ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true).ToList(), "ProducerID", "CompanyName");
                ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");
                //ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "Number");
                ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");
                return View();


            }






            // return View();
        }

        [Authorize(Roles = "Consumer")]
        public ActionResult ShowReportConsumer()
        {
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true).ToList(), "ProducerID", "CompanyName");
            //ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName");
            ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "Number");
            ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");

            return View();
        }

          [Authorize(Roles = "Consumer")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ShowReportConsumer(T_BurdenInformations t_burden, FormCollection frm)
        {
            if (frm["ProducerID"].ToString() != "" || frm["ProduceID"].ToString() != "" || frm["TruckID"].ToString() != "" || frm["FromDate"].ToString() != "" || frm["ToDate"].ToString() != "")
            {
                long producerid = 0;
                long produceid = 0;
               // long consumerID = 0;
                long truckID = 0;
                string fromDate = "", toDate = "";
                try
                {
                    producerid = long.Parse(frm["ProducerID"].ToString());
                }
                catch
                {

                }
                try
                {
                    produceid = long.Parse(frm["ProduceID"].ToString());
                }
                catch
                {

                }
                
                try
                {
                    truckID = long.Parse(frm["TruckID"].ToString());
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



                return RedirectToAction("ReportConsumer", "BurdenInformation", new { ProducerID = producerid,  ProduceID = produceid, TruckID = truckID, FromDate = fromDate, ToDate = toDate });
            }
            else
            {
                ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true).ToList(), "ProducerID", "CompanyName");
                //ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName");
                ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "Number");
                ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");
                return View();


            }






            // return View();
        }


          [Authorize(Roles = "HighUser,Administrator")]
          public ActionResult GetReportSnapshot(string ProducerID, string ConsumerID, string ProduceID, string TruckID, string FromDate, string ToDate)
        {
            //if (ProducerID != 0 || ConsumerID != 0 || ProduceID != 0 || TruckID != 0 || FromDate != null || ToDate != null)
            //{
                StiReport report = new StiReport();
                
                string path = Server.MapPath("~/Reports/bi-list.mrt");
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
                String where ="";
                if (ProducerID != null) { 
                    where += " and  ProducerID IN (" + ProducerID.ToString()+")";
                }
                if (ProduceID != null)
                {
                    where += " and  ProduceID IN (" + ProduceID.ToString() + ")";
                }
                if (ConsumerID != null)
                {
                    where += " and  ConsumerID IN (" + ConsumerID.ToString() + ")";
                }
                if (TruckID != null)
                {
                    where += " and  TruckID IN (" + TruckID.ToString() + ")";
                }

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) <='" + ToDate + "'";
                }
                report["cnd"] = where;
                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            //}
            //else
            //{
            //    return RedirectToAction("index");
            //}
        }

        [Authorize(Roles = "User,LowUser")]
        public ActionResult GetReportSnapshot1(long ProducerID, long ConsumerID, long ProduceID, long TruckID, string FromDate, string ToDate)
        {
            if (ProducerID != 0 || ConsumerID != 0 || ProduceID != 0 || TruckID != 0 || FromDate != null || ToDate != null)
            {
                StiReport report = new StiReport();

                string path = Server.MapPath("~/Reports/bi-list1.mrt");
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
                    where += " and  ProducerID =" + ProducerID.ToString();
                }
                if (ProduceID != 0)
                {
                    where += " and  ProduceID =" + ProduceID.ToString();
                }
                if (ConsumerID != 0)
                {
                    where += " and  ConsumerID =" + ConsumerID.ToString();
                }
                if (TruckID != 0)
                {
                    where += " and  TruckID =" + TruckID.ToString();
                }

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) <='" + ToDate + "'";
                }
                report["cnd"] = where;
                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

          [Authorize(Roles = "Truck")]
        public ActionResult GetReportSnapshotTruck(long ProducerID, long ConsumerID, long ProduceID, string FromDate, string ToDate)
        {
            if (ProducerID != 0 || ConsumerID != 0 || ProduceID != 0 ||  FromDate != null || ToDate != null)
            {
                StiReport report = new StiReport();

                string path = Server.MapPath("~/Reports/bi-listTruck.mrt");
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
                    where += " and  ProducerID =" + ProducerID.ToString();
                }
                if (ProduceID != 0)
                {
                    where += " and  ProduceID =" + ProduceID.ToString();
                }
                if (ConsumerID != 0)
                {
                    where += " and  ConsumerID =" + ConsumerID.ToString();
                }
                
                    

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) <='" + ToDate + "'";
                }
                where += " and  TruckID =" + db.T_Users.Where(m=> m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID.ToString();
               
                report["cnd"] = where;
                //report.Render();
                report.Render();
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

          [Authorize(Roles = "Consumer")]
        public ActionResult GetReportSnapshotConsumer(long ProducerID, long ProduceID, long TruckID, string FromDate, string ToDate)
        {
            if (ProducerID != 0 ||  ProduceID != 0 || TruckID != 0 || FromDate != null || ToDate != null)
            {
                StiReport report = new StiReport();

                string path = Server.MapPath("~/Reports/bi-listConsumer.mrt");
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
                    where += " and  ProducerID =" + ProducerID.ToString();
                }
                if (ProduceID != 0)
                {
                    where += " and  ProduceID =" + ProduceID.ToString();
                }
                
                   
                
                if (TruckID != 0)
                {
                    where += " and  TruckID =" + TruckID.ToString();
                }

                if (FromDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) >='" + FromDate + "'";
                }
                if (ToDate != null)
                {
                    where += " and  SUBSTRING (UnLoadDateTime,1,10) <='" + ToDate + "'";
                }
                where += " and  ConsumerID =" + db.T_Users.Where(m => m.UserName.Equals(User.Identity.Name)).ToList()[0].PublicID.ToString();
               
                report["cnd"] = where;
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
        public ActionResult Report()
        {




            return View();
        }


          [Authorize(Roles = "User,LowUser")]
        public ActionResult Report1()
        {




            return View();
        }
          [Authorize(Roles = "Truck")]
        public ActionResult ReportTruck()
        {




            return View();
        }
          [Authorize(Roles = "Consumer")]
        public ActionResult ReportConsumer()
        {




            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ShowReport(long ProducerID,long ProduceID,long ConsumerID,long TruckID)
        //{

        //    if (ProduceID != 0)
        //    {

        //    }


        //    ViewBag.ProducerID = new SelectList(db.T_Producers.ToList(), "ProducerID", "CompanyName");
        //    ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName");
        //    ViewBag.TruckID = new SelectList(db.T_Trucks.ToList(), "TruckID", "Number");
        //    ViewBag.ProduceID = new SelectList(db.T_Produces.ToList(), "ProduceID", "ProduceName");





        //    return View();
        //}

        //
        // GET: /Default1/Details/5

        [Authorize(Roles = "HighUser,Administrator,User")]
        public ActionResult Details(long id = 0)
        {
            T_BurdenInformations t_burdeninformations = db.T_BurdenInformations.Find(id);
            if (t_burdeninformations == null)
            {
                return HttpNotFound();
            }
            return View(t_burdeninformations);
        }

          [Authorize(Roles = "HighUser,Administrator,User")]
        public JsonResult getActiveTransportPrice_Produce(int ProducerID,int ConsumerID)
        {
            List<T_TransportPrices> list =db.T_TransportPrices.Where(m => m.StateID == true && m.ProducerID ==ProducerID && m.ConsumerID==ConsumerID).ToList();
            List<Produce> proList = new List<Produce>();
            if (list.Count > 0)
            {
                for (int i=0;i<list.Count;i++)
                {
                    Produce p = new Produce();
                    p.ProduceID = list[i].T_Produces.ProduceID;
                    p.ProduceName = list[i].T_Produces.ProduceName;

                    proList.Add(p);
                }
            }
            return Json(proList);
        }



          [Authorize(Roles = "HighUser,Administrator,User")]
        public string getTruckOwnerName(int TruckID)
        {

            int Tr = TruckID;
            if (Tr != null)
            {
                T_Trucks tr = db.T_Trucks.Find(TruckID);
                return tr.OwnerFullName;
            }
            else
            {
                return "";
            }

            //tr.OwnerFullName;


        }
          [Authorize(Roles = "HighUser,Administrator,User")]
        public JsonResult getActiveTransportPrice_Price(int ProducerID, int ConsumerID,int ProduceID)
        {
            
            List<T_TransportPrices> list = db.T_TransportPrices.Where(m => m.StateID == true && m.ProducerID == ProducerID && m.ConsumerID == ConsumerID && m.ProduceID ==ProduceID).ToList();
            List<TransportPrice> trList = new List<TransportPrice>();
            if (list.Count == 1)
            {
                TransportPrice tr = new TransportPrice();
                tr.TransportPriceID = list[0]._TransportPriceID;
                tr.Price = list[0].Price;
                trList.Add(tr);
            }
            return Json(trList);
        }
          [Authorize(Roles = "HighUser,Administrator,User")]
          public double getActivePPP_Price(int ProducerID, int ProduceID)
        {
            decimal price = 0;
            List<T_ProducerProducePrices> p = db.T_ProducerProducePrices.Where(m => m.StateID == true && m.ProducerID == ProducerID &&  m.ProduceID == ProduceID).ToList();
            if (p.Count == 1)
            {
                price = p[0]._Price;
            }
            return double.Parse (price.ToString());
        }
          [Authorize(Roles = "HighUser,Administrator,User")]
          public double getActiveCPP_Price(int ConsumerID, int ProduceID)
        {
            decimal price = 0;
            List<T_ConsumerProducePrices> p = db.T_ConsumerProducePrices.Where(m => m.StateID == true && m.ProduceID == ProduceID && m.ConsumerID==ConsumerID).ToList();
            if (p.Count == 1)
            {
                price = p[0]._Price;
            }

            return double.Parse(price.ToString());
        }

          [Authorize(Roles = "HighUser,Administrator,User")]
          public JsonResult getActivePPP_Prices(int ProducerID, int ProduceID)
          {
              List<ProducerPP> prices =new List<ProducerPP>();
              List<T_ProducerProducePrices> p = db.T_ProducerProducePrices.Where(m=> m.ProducerID == ProducerID && m.ProduceID == ProduceID).ToList();
              for (int i = 0; i < p.Count; i++)
              {
                  ProducerPP ppp = new ProducerPP();
                  ppp.ProducerProducePriceID = p[i]._ProducerProducePriceID;
                  ppp.Price = p[i]._Price;

                  prices.Add(ppp);
              }

                  return Json(prices);
          }
          [Authorize(Roles = "HighUser,Administrator,User")]
          public JsonResult getActiveCPP_Prices(int ConsumerID, int ProduceID)
          {
              List<ConsumerPP> prices = new List<ConsumerPP>();
              List<T_ConsumerProducePrices> p = db.T_ConsumerProducePrices.Where(m=> m.ProduceID == ProduceID && m.ConsumerID == ConsumerID).ToList();
              for (int i = 0; i < p.Count; i++)
              {
                  ConsumerPP ppp = new ConsumerPP();
                  ppp.ConsumerProducePriceID = p[i].ConsumerProducePriceID;
                  ppp.Price = p[i]._Price;

                  prices.Add(ppp);
              }

              return Json(prices);
          }

        //
        // GET: /Default1/Create
          [Authorize(Roles = "HighUser,Administrator,User")]
        public ActionResult Create()
        {
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m=>m.StateID==true).ToList(), "ProducerID", "CompanyName");
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");

            ViewBag.TransportPriceID = new SelectList(db.T_TransportPrices.Where(m => m.TransportPriceID==0), "TransportPriceID", "_TransportPriceName");
            //ViewBag.TransportPriceID = new SelectList(db.T_TransportPrices.Where(m => m.StateID == true), "TransportPriceID", "_TransportPriceName");

            ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "_NumberName");
            ViewBag.UserID = new SelectList(db.T_Users, "UserID", "UserName");


            return View();
        }

        //
        // POST: /Default1/Create
          [Authorize(Roles = "HighUser,Administrator,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_BurdenInformations t_burdeninformations)
        {
            if (ModelState.IsValid)
            {
                int ProduceID = db.T_TransportPrices.Where(m => m.TransportPriceID == t_burdeninformations.TransportPriceID).ToList()[0].ProduceID;
                int ProducerID = db.T_TransportPrices.Where(m => m.TransportPriceID == t_burdeninformations.TransportPriceID).ToList()[0].ProducerID;
                int ConsumerID = db.T_TransportPrices.Where(m => m.TransportPriceID == t_burdeninformations.TransportPriceID).ToList()[0].ConsumerID;

                List<T_ProducerProducePrices> ppps = db.T_ProducerProducePrices.Where(m => m.ProduceID == ProduceID && m.ProducerID == ProducerID && m.StateID == true).ToList();
                List<T_ConsumerProducePrices> cpps = db.T_ConsumerProducePrices.Where(m => m.ProduceID == ProduceID && m.ConsumerID == ConsumerID && m.StateID == true).ToList();


                if (ppps.Count == 1)
                {
                    t_burdeninformations.PPPID = ppps[0].ProducerProducePriceID;
                }
                else if (ppps.Count > 1)
                {
                    ViewBag.err = " بیشتر از یک قیمت محصول تولید کننده فعال است ابتدا به لیست قیمت محصولات تولید کننده رفته و پس از رفع مشکل دوباره تلاش کنید" ;
                    return View();
                }
                else if (ppps.Count == 0)
                {
                    ViewBag.err = " هیچ یک از قیمت های محصول تولید کننده فعال نیست ابتدا به لیست قیمت محصولات تولید کننده رفته و پس از رفع مشکل دوباره تلاش کنید";
                    return View();
                }
                if (cpps.Count == 1)
                {
                    t_burdeninformations.CPPID = cpps[0].ConsumerProducePriceID;
                }
                else if (cpps.Count > 1)
                {
                    ViewBag.err = " بیشتر از یک قیمت محصول خریدار فعال است ابتدا به لیست قیمت محصولات خریدار رفته و پس از رفع مشکل دوباره تلاش کنید";
                    return View();
                }
                else if (cpps.Count == 0)
                {
                    ViewBag.err = " هیچ یک از قیمت های محصول خریدار فعال نیست ابتدا به لیست قیمت محصولات خریدار رفته و پس از رفع مشکل دوباره تلاش کنید";
                    return View();
                }





                t_burdeninformations.RegisterDateTime = PersianDateTime.Now.ToDateTime().ToShortDateString();
                //t_burdeninformations.UserID =int.Parse( Session["UserID"].ToString());
                t_burdeninformations.UserID = db.T_Users.Where(m => m.UserName == User.Identity.Name).First().UserID;
                
                db.T_BurdenInformations.Add(t_burdeninformations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerID = new SelectList(db.T_Producers.Where(m => m.StateID == true).ToList(), "ProducerID", "CompanyName");
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.Where(m => m.StateID == true).ToList(), "ConsumerID", "CompanyName");

            ViewBag.TransportPriceID = new SelectList(db.T_TransportPrices.Where(m => m.StateID == true), "TransportPriceID", "_TransportPriceName");
            ViewBag.TruckID = new SelectList(db.T_Trucks.Where(m => m.StateID == true).ToList(), "TruckID", "_NumberName", t_burdeninformations.TruckID);
            //ViewBag.UserID = new SelectList(db.T_Users, "UserID", "UserName", t_burdeninformations.UserID);
            //return View(t_burdeninformations);
            return RedirectToAction("Create");

        }

        //
        // GET: /Default1/Edit/5
          [Authorize(Roles = "HighUser,Administrator,User")]
        public ActionResult Edit(long id = 0)
        {
            T_BurdenInformations t_burdeninformations = db.T_BurdenInformations.Find(id);
            if (t_burdeninformations == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProducerID = new SelectList(db.T_Producers.ToList(), "ProducerID", "CompanyName", t_burdeninformations.T_TransportPrices.ProducerID);
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", t_burdeninformations.T_TransportPrices.ConsumerID);
            ViewBag.PPPID = new SelectList(db.T_ProducerProducePrices.Where(m => m.ProduceID == t_burdeninformations.T_TransportPrices.ProduceID && m.ProducerID == t_burdeninformations.T_TransportPrices.ProducerID).ToList(), "ProducerProducePriceID", "Price", t_burdeninformations.PPPID);
            ViewBag.CPPID = new SelectList(db.T_ConsumerProducePrices.Where(m => m.ProduceID == t_burdeninformations.T_TransportPrices.ProduceID && m.ConsumerID == t_burdeninformations.T_TransportPrices.ConsumerID).ToList(), "ConsumerProducePriceID", "Price", t_burdeninformations.CPPID);
            ViewBag.BID = id;

            // m.StateID == true &&
            List<T_TransportPrices> list = db.T_TransportPrices.Where(m => m.ProducerID == t_burdeninformations.T_TransportPrices.ProducerID && m.ConsumerID == t_burdeninformations.T_TransportPrices.ConsumerID).ToList();
            List<Produce> proList = new List<Produce>();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Produce p = new Produce();
                    p.ProduceID = list[i].T_Produces.ProduceID;
                    p.ProduceName = list[i].T_Produces.ProduceName;

                    proList.Add(p);
                }
            }
            ViewBag.ProduceID = new SelectList(proList, "ProduceID", "ProduceName", t_burdeninformations.T_TransportPrices.ProduceID);

            ViewBag.TransportPriceID = new SelectList(db.T_TransportPrices.Where(m => m.ConsumerID == t_burdeninformations.T_TransportPrices.ConsumerID && m.ProducerID == t_burdeninformations.T_TransportPrices.ProducerID && m.ProduceID == t_burdeninformations.T_TransportPrices.ProduceID), "TransportPriceID", "Price", t_burdeninformations.TransportPriceID);
            ViewBag.TruckID = new SelectList(db.T_Trucks, "TruckID", "_NumberName", t_burdeninformations.TruckID);
           // ViewBag.UserID = new SelectList(db.T_Users, "UserID", "UserName");

            return View(t_burdeninformations);
        }

        //
        // POST: /Default1/Edit/5
          [Authorize(Roles = "HighUser,Administrator,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Exclude = "CFSID,PFSID,TFSID")]
        public ActionResult Edit( T_BurdenInformations t_burdeninformations)
        {
            if (ModelState.IsValid)
            {
                long bid = t_burdeninformations.BurdenInformationID;

                int ProduceID = db.T_TransportPrices.Where(m => m.TransportPriceID == t_burdeninformations.TransportPriceID).ToList()[0].ProduceID;
                int ProducerID = db.T_TransportPrices.Where(m => m.TransportPriceID == t_burdeninformations.TransportPriceID).ToList()[0].ProducerID;
                int ConsumerID = db.T_TransportPrices.Where(m => m.TransportPriceID == t_burdeninformations.TransportPriceID).ToList()[0].ConsumerID;

                //long? CFSID, PFSID, TFSID;

                //CFSID = db.T_BurdenInformations.Where(m => m.BurdenInformationID == bid).ToList()[0].CFSID;
                //PFSID = db.T_BurdenInformations.Where(m => m.BurdenInformationID == bid).ToList()[0].PFSID;
                //TFSID = db.T_BurdenInformations.Where(m => m.BurdenInformationID == bid).ToList()[0].TFSID;



                List<T_ProducerProducePrices> ppps = db.T_ProducerProducePrices.Where(m => m.ProduceID == ProduceID && m.ProducerID == ProducerID && m.StateID == true).ToList();
                List<T_ConsumerProducePrices> cpps = db.T_ConsumerProducePrices.Where(m => m.ProduceID == ProduceID && m.ConsumerID == ConsumerID && m.StateID == true).ToList();

                if (ppps.Count == 1)
                {
                    t_burdeninformations.PPPID = ppps[0].ProducerProducePriceID;
                }
                else if (ppps.Count > 1)
                {
                    ViewBag.err = " بیشتر از یک قیمت محصول تولید کننده فعال است ابتدا به لیست قیمت محصولات تولید کننده رفته و پس از رفع مشکل دوباره تلاش کنید";
                    return View();
                }
                else if (ppps.Count == 0)
                {
                    ViewBag.err = " هیچ یک از قیمت های محصول تولید کننده فعال نیست ابتدا به لیست قیمت محصولات تولید کننده رفته و پس از رفع مشکل دوباره تلاش کنید";
                    return View();
                }
                if (cpps.Count == 1)
                {
                    t_burdeninformations.CPPID = cpps[0].ConsumerProducePriceID;
                }
                else if (cpps.Count > 1)
                {
                    ViewBag.err = " بیشتر از یک قیمت محصول خریدار فعال است ابتدا به لیست قیمت محصولات خریدار رفته و پس از رفع مشکل دوباره تلاش کنید";
                    return View();
                }
                else if (cpps.Count == 0)
                {
                    ViewBag.err = " هیچ یک از قیمت های محصول خریدار فعال نیست ابتدا به لیست قیمت محصولات خریدار رفته و پس از رفع مشکل دوباره تلاش کنید";
                    return View();
                }
                else
                {

                }
                t_burdeninformations.RegisterDateTime = PersianDateTime.Now.ToDateTime().ToShortDateString();
                t_burdeninformations.UserID = db.T_Users.Where(m=>m.UserName == User.Identity.Name).First().UserID;
                //t_burdeninformations.UserID = int.Parse(Session["UserID"].ToString());
                //t_burdeninformations.PFSID = PFSID;
                //t_burdeninformations.CFSID = CFSID;
                //t_burdeninformations.TFSID = TFSID;

                db.Entry(t_burdeninformations).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerID = new SelectList(db.T_Producers.ToList(), "ProducerID", "CompanyName", t_burdeninformations.PPPID);
            ViewBag.ConsumerID = new SelectList(db.T_Consumers.ToList(), "ConsumerID", "CompanyName", t_burdeninformations.CPPID);
            ViewBag.PPPID = new SelectList(db.T_ProducerProducePrices.Where(m => m.ProduceID == t_burdeninformations.T_TransportPrices.ProduceID && m.ProducerID == t_burdeninformations.T_TransportPrices.ProducerID).ToList(), "ProducerProducePriceID", "Price", t_burdeninformations.PPPID);
            ViewBag.CPPID = new SelectList(db.T_ConsumerProducePrices.Where(m => m.ProduceID == t_burdeninformations.T_TransportPrices.ProduceID && m.ConsumerID == t_burdeninformations.T_TransportPrices.ConsumerID).ToList(), "ConsumerProducePriceID", "Price", t_burdeninformations.CPPID);
             
            ViewBag.TransportPriceID = new SelectList(db.T_TransportPrices.ToList(), "TransportPriceID", "_TransportPriceName", t_burdeninformations.TransportPriceID);
            ViewBag.TruckID = new SelectList(db.T_Trucks, "TruckID", "_NumberName", t_burdeninformations.TruckID);

            return View(t_burdeninformations);
        }

        //
        // GET: /Default1/Delete/5
          [Authorize(Roles = "HighUser,Administrator,User")]
        public ActionResult Delete(long id = 0)
        {
            T_BurdenInformations t_burdeninformations = db.T_BurdenInformations.Find(id);
            if (t_burdeninformations == null)
            {
                return HttpNotFound();
            }
            return View(t_burdeninformations);
        }

        //
        // POST: /Default1/Delete/5
          [Authorize(Roles = "HighUser,Administrator,User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_BurdenInformations t_burdeninformations = db.T_BurdenInformations.Find(id);
            db.T_BurdenInformations.Remove(t_burdeninformations);
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