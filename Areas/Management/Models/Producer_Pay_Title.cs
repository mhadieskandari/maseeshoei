using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_Producer_Pay_Title
    {
        [Display(Name = "TitleID")]
        public short _TitleID { get; set; }

        [Display(Name = "عنوان پرداخت")]
        public string _TitleName { get; set; }
    }
}