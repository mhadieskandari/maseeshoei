using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_ConsumersPays
    {
        [Display(Name="ID")]
        [Required]
        public long _ConsumerPayID { get { return ConsumerPayID; } set { ConsumerPayID = value; } }

        [Display(Name = "کد سیستم صورت حساب")]
        [Required]
        public long _CFSID { get { return CFSID; } set { CFSID = value; } }

        [Display(Name = "تاریخ سر رسید")]
        public string _PayDate { get { return PayDate; } set { PayDate = value; } }

        [Display(Name = "تاریخ دریافت چک")]
        public string _RegisterDate { get { return RegisterDate; } set { RegisterDate = value; } }


        [Required]
        [Display(Name = "مبلغ")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public double _Price { get { return double.Parse( Price.ToString()); } set { Price =decimal.Parse( value.ToString()); } }

         [Required]
        [StringLength(20)]
        [Display(Name = "شماره رسید/چک")]
        public string _Number { get { return Number; } set { Number = value; } }

        [Display(Name = "توضیحات")]
        public string _Description { get { return Description; } set { Description = value; } }

        [Display(Name = "صاحب چک/مدرک")]
        public string _Owner { get { return Owner; } set { Owner = value; } }

        [Display(Name = "شعبه بانک")]
        public string _Bankdepartment { get { return Bankdepartment; } set { Bankdepartment = value; } }

        [Display(Name = "تاریخ وصول")]
        public string _RecoveryDate { get { return RecoveryDate; } set { RecoveryDate = value; } }

        
       
        
        
       
       
        
        



    }
}