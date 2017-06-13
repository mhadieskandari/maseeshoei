using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public class DataBase
    {
        public string DBName{set;get;}
        public string Text { set; get; }

        public List<DataBase> GetDataDases()
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