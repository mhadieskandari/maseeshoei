using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_State
    {
        [Display(Name="وضعیت")]
        public bool _StateID { get { return StateID; } set { StateID=value;} }
        [Display(Name = "وضعیت")]
        public string _StateName { get { return StateName; } set { StateName = value; } }
    }
}