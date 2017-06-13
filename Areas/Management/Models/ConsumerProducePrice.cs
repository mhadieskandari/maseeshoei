using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_ConsumerProducePrices
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        public int _ConsumerProducePriceID { set { ConsumerProducePriceID = value; } get { return ConsumerProducePriceID; } }

        [Required]
        [Display(Name = "مشتری", AutoGenerateField = true)]
        public int _ConsumerID { set { ConsumerID = value; } get { return ConsumerID; } }
        //public Consumer Consumer { set {=value ;} get{return;} }

        [Required]
        [Display(Name = "محصول")]
        public int _ProduceID { set { ProduceID = value; } get { return ProduceID; } }
        //public Produce Produce { set {=value ;} get{return;} }

        [Required]
        //[DataType(DataType.DateTime)]
        [Display(Name = "تاریخ")]
        public string _Date { set { Date = value; } get { return Date; } }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}",ApplyFormatInEditMode=true)]
        [Display(Name = "فی فروش")]
        public decimal _Price { set { Price = value; } get { return Price; } }

        [Display(Name = "وضعیت")]
        public bool _StateID { set { StateID = value; } get { return StateID; } }

        [Display(Name = "وضعیت")]
        public string _StateName {  get { return T_State.StateName; } }
    }
}