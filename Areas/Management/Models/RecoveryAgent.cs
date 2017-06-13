using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaaseShooei.Areas.Management.Models
{
    public partial class T_RecoveryAgent
    {
        [Display(Name = "AgentID")]
        public short _AgentID { get; set; }
       

        [Display(Name = "مامور وصول")]
        public string _AgentName { get { return AgentName; } set { AgentName = value; } }

    }
}