using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_Consumer_Bank
    {

        [Display(Name = "BankID")]
        public short _BankID { get { return BankID; } set { BankID = value; } }

        [Display(Name = "نام بانک ")]
        public string _BankName { get { return BankName; } set { BankName = value; } }
    }
}