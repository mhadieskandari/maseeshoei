using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_ProducerProducePrices
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name="ID")]
        public int _ProducerProducePriceID { set { ProducerProducePriceID = value; } get { return ProducerProducePriceID; } }

        [Display(Name = "شرکت تولید کننده")]
        //public int ProducerID { set { =value ;} get {return  ;} }
        public int _ProducerID { set { ProducerID = value; } get { return ProducerID; } }

        [Display(Name = "شرکت تولید کننده")]
        //public int ProducerID { set { =value ;} get {return  ;} }
        public string _ProducerName { set { T_Produces.ProduceName = value; } get { return T_Produces.ProduceName; } }
        
        [Display(Name = "نام محصول")]
        public int _ProduceID { set { ProduceID = value; } get { return ProduceID; } }

        [Display(Name = "تاریخ")]
        //[DataType(DataType.DateTime)]
        public string _Date { set { Date = value; } get { return Date; } }

        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "فی تولید")]
        public decimal _Price { set { Price = value; } get { return Price; } }

        [Display(Name = "وضعیت")]
        public bool _State { set { StateID = value; } get { return StateID; } }

        //[Display(Name = "وضعیت")]
        //public int _StateName { set { State = value; } get { return State; } }
    }
}