using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_Consumers
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        public int _ConsumerID { set { ConsumerID = value; } get { return ConsumerID; } }

        [Required]
        [MaxLength(30)]
        [Display(Name = "خریدار")]
        public string _CompanyName { set { CompanyName = value; } get { return CompanyName; } }

        //[Required]
        [MaxLength(30)]
        [Display(Name = "صاحب خریدار")]
        public string _OwnerFullName { set { OwnerFullName = value; } get { return OwnerFullName; } }

        //[Required]
        [MaxLength(150)]
        [Display(Name = "آدرس")]
        public string _Address { set { Address = value; } get { return Address; } }

       // [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(35)]
        [Display(Name = "تلفن")]
        public string _PhoneNumber { set { PhoneNumber = value; } get { return PhoneNumber; } }

        [MaxLength(100)]
        [Display(Name = "توضیحات")]
        public string _Descryption { set { Descryption = value; } get { return Descryption; } }

        [MaxLength(20), MinLength(8)]
        [Display(Name = "نام کاربری")]
        public string _UserName { set { UserName = value; } get { return UserName; } }

        //[Required]
        [MaxLength(20), MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string _PassWord { set { PassWord = value; } get { return PassWord; } }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [Display(Name = "ایمیل")]
        public string _EmailAddress { set { EmailAddress = value; } get { return EmailAddress; } }
    }
}