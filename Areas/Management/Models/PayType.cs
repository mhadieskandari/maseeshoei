using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_PayType
    {
    
        [Display(Name="id")]
        public short _PayTypeID { get { return PayTypeID; } set { PayTypeID = value; } }


        [Display(Name = "نوع پرداخت")]
        public string _PayTypeName { get { return PayTypeName; } set { PayTypeName = value; } }
    }
}