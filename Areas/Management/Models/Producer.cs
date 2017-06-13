using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    [Serializable]
    public partial class T_Producers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="ID")]
        [Required]
        public int _ProducerID { set { ProducerID = value; } get { return ProducerID; } }

        [Required]
        [MinLength(5), MaxLength(50)]
        [Display(Name = "تولید کننده")]
        public string _CompanyName { set { CompanyName = value; } get { return CompanyName; } }

        //[Required]
        [MinLength(5), MaxLength(50)]
        [Display(Name = "صاحب تولید کننده")]
        public string _OwnerFullName { set { OwnerFullName = value; } get { return OwnerFullName; } }

        //[Required]
        [MaxLength(100)]
        [Display(Name = "آدرس")]
        public string _Address { set { Address = value; } get { return Address; } }

        //[Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(35)]
        [Display(Name = "تلفن")]
        public string _PhoneNumber { set { PhoneNumber = value; } get { return PhoneNumber; } }

        [Display(Name = "توضیحات")]
        [MaxLength(200)]
        public string _Descryption { set { Descryption = value; } get { return Descryption; } }

       
    }
}