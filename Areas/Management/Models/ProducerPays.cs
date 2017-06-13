using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_ProducersPays
    {
        [Display(Name = "ID")]
        [Required]
        public long _ProducerPayID { get { return ProducerPayID; } set { ProducerPayID = value; } }


        [Required]
        [Display(Name = "کد سیستم صورتحساب تولید کننده")]
        public long _PFSID { get { return PFSID; } set { PFSID = value; } }

        [Display(Name = "تاریخ سر رسید")]
        public string _PayDate { get { return PayDate; } set { PayDate = value; } }

        [Display(Name = "تاریخ صدور چک")]
        public string _RegisterDate { get { return RegisterDate; } set { RegisterDate = value; } }

         [Required]
         [DataType(DataType.Currency)]
        [Display(Name = "مبلغ")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public double _Price { get { return double.Parse(Price.ToString()); } set { Price = decimal.Parse(value.ToString()); } }

         [Required]
        [Display(Name = "شماره رسید/چک")]
        [StringLength(20)]
        public string _Number { get { return Number; } set { Number = value; } }

        [Display(Name = "توضیحات")]
        public string _Description { get { return Description; } set { Description = value; } }


      
       
        [Display(Name = "تاریخ وصول")]
        public string _RecoveryDate { get { return RecoveryDate; } set { RecoveryDate = value; } }

        
       
        









    }
}