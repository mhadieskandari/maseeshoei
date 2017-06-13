using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_Users
    {
        [Key]
        [Display(Name = "ID")]
        public int _UserID { get { return UserID; } set { UserID = value; } }

        [Required]
        [Display(Name = "نام کاربری")]
        public string _UserName { get { return UserName; } set { UserName = value; } }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور")]
        public string _PassWord { get { return PassWord; } set { PassWord = value; } }

        [Required]
        [Display(Name = "نام")]
        public string _FirstName { get { return FirstName; } set { FirstName = value; } }

        [Required]
        [Display(Name = "نام خانوادگی")]
        public string _LastName { get { return LastName; } set { LastName = value; } }

        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "ایمیل")]
        public string _EmailAddress { get { return EmailAddress; } set { EmailAddress = value; } }

        [MaxLength(35)]
        [Display(Name = "شماره تلفن")]
        public string _PhoneNumber { get { return PhoneNumber; } set { PhoneNumber = value; } }

        [Required]
        [Display(Name = "نوع کاربر")]
        public short? _TypeID { get { return TypeID; } set { TypeID = value; } }
    }
}