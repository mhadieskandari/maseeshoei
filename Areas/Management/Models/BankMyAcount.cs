using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_BankMyAcount
    {
        [Display(Name = "BankID")]
        public short _BankId { get { return BankId; } set { BankId = value; } }

        [Display(Name = "نام بانک ")]
        public string _BankName { get { return BankName; } set { BankName = value; } }

        [Display(Name = "شماره حساب")]
        public string _AccountNumber { get { return AccountNumber; } set { AccountNumber = value; } }

        [Display(Name = "نام بانک / شماره حساب")]
        public string _BankNumber { get { return BankName +" "+AccountNumber; } }
    }
}