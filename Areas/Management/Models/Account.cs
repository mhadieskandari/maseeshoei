using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public class Account
    {
        [Display(Name="نام کاربری")]
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [StringLength(20)]
        public string Username { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        
        public bool RememberMe { get; set; }
    }
}