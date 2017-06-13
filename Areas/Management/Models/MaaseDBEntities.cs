using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class MaaseDBEntities : DbContext
    {
        
        
        
        public MaaseDBEntities()
            : base("name=" + HttpContext.Current.Session["DBConnection"].ToString())
        {

        }

    }
}