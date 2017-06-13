using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public static class Statics
    {
        public static string dbconnection { set; get; }
        public static string reportconnection { set; get; }
        public static string DBYearName { set; get; }

       
            
        
        

        public static List<DataBase> GetDataDases()
        {
            List<DataBase> dbList = new List<DataBase>();

            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[i].ConnectionString);
                if (ConfigurationManager.ConnectionStrings[i].Name.Contains("MaaseDBEntities"))
                {
                    DataBase db = new DataBase();
                    db.DBName = ConfigurationManager.ConnectionStrings[i].Name;
                    db.Text = "سال " + ConfigurationManager.ConnectionStrings[i].Name.Substring(15);
                    dbList.Add(db);
                }
            }



            return dbList;
        }


    }
}