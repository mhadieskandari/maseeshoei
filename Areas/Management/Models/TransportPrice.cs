using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_TransportPrices
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        public int _TransportPriceID { set { TransportPriceID = value; } get { return TransportPriceID; } }

        [Display(Name = "تولید کننده")]
        public int _ProducerID { set { ProducerID = value; } get { return ProducerID; } }
        //public Producer Producer { set { =value ;} get {return ;} }

        [Display(Name = "خریدار")]
        public int _ConsumerID { set { ConsumerID = value; } get { return ConsumerID; } }
        //public Consumer Consumer { set { =value ;} get {return ;} }

        [Display(Name = "محصول")]
        public int _ProduceID { set { ProduceID = value; } get { return ProduceID; } }
        //public Produce Produce { set { =value ;} get {return ;} }

        [Display(Name = "تاریخ")]
        //[DataType(DataType.DateTime)]
        public string _Date { set { Date = value; } get { return Date; } }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "مبلغ")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal _Price { set { Price = value; } get { return Price; } }

        

        [Display(Name = "وضعیت")]
        public bool _StateID { set { StateID = value; } get { return StateID; } }

        [Display(Name = "وضعیت")]
        public string _StateName {  get { return T_State.StateName; } }

    }
}