using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_TrucksPays
    {
         [Required]
        [Display(Name = "کد سیستم پرداخت")]
        public long _TruckPayID { get { return TruckPayID; } set { TruckPayID = value; } }

         [Required]
        [Display(Name = "کد سیستم صورتحساب رانندگان")]
        public long _TFSID { get { return TFSID; } set { TFSID = value; } }

        [Display(Name = "تاریخ سررسید")]
        public string _PayDate { get { return PayDate; } set { PayDate = value; } }

        [Display(Name = "تاریخ صدور چک")]
        public string _RegisterDate { get { return RegisterDate; } set { RegisterDate = value; } }

         [Required]
         [DataType(DataType.Currency)]
        [Display(Name = "مبلغ")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public double _Price { get { return double.Parse(Price.ToString()); } set { Price = decimal.Parse(value.ToString()); } }


        [Display(Name = "شماره چک/مدرک")]
        [StringLength(20)]
        [Required]
        public string _Number { get { return Number; } set { Number = value; } }

        [Display(Name = "توضیحات")]
        public string _Description { get { return Description; } set { Description = value; } }
               
        [Display(Name = "تاریخ وصول")]
        public string _RecoveryDate { get { return RecoveryDate; } set { RecoveryDate = value; } }

        
        
       
        









    }
}