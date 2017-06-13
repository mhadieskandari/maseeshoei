using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_PayState
    {

        [Display(Name="id")]
        public short _PayStateID { get { return PayStateID; } set { PayStateID = value; } }

         [Display(Name = "وضعیت")]
        public string _PayStateName { get { return PayStateName; } set { PayStateName = value; } }
    }
}