using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_Produces
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name="ID")]
        public int _ProduceID { set {ProduceID=value ;} get {return ProduceID ;} }

        [Required]
        [ MaxLength(30)]
        [Display(Name = "نام محصول")]
        public string _ProduceName { set {ProduceName=value ;} get {return ProduceName ;} }

        [MaxLength(100)]
        [Display(Name = "توضیحات")]
        public string _Descryption { set {Descryption=value ;} get {return Descryption ;} }
    }
}