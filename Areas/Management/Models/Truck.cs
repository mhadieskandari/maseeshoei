using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_Trucks
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        public int _TruckID { set { TruckID = value; } get { return TruckID; } }

        [Required]
        [StringLength(5)]
        [Display(Name = "شماره ماشین")]
        
        public string _Number { set {Number=value ;} get {return Number ;} }

        [Required]
        [Display(Name = "صاحب ماشین")]
        [MaxLength(30)]
        public string _OwnerFullName { set {OwnerFullName=value ;} get {return OwnerFullName ;} }

        [Required]
        [Display(Name = "تلفن")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(35)]
        public string _PhoneNumber { set {PhoneNumber=value ;} get {return PhoneNumber ;} }

        [Required]
        [Display(Name = "وزن خالی")]
        public int? _PureWeight { set {PureWeight=value ;} get {return PureWeight ;} }

        [Display(Name = "توضیحات")]
        [MaxLength(300)]
        public string _Descryption { set {Descryption=value ;} get {return Descryption ;} }

        [Display(Name = "ماشین")]
        public string _NumberName { get { return Number +" "+OwnerFullName; } }



    }
}