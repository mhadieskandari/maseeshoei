//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaaseShooei.Areas.Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Producer_Pay_Title
    {
        public T_Producer_Pay_Title()
        {
            this.T_ProducersPays = new HashSet<T_ProducersPays>();
        }
    
        public short TitleID { get; set; }
        public string TitleName { get; set; }
    
        public virtual ICollection<T_ProducersPays> T_ProducersPays { get; set; }
    }
}
