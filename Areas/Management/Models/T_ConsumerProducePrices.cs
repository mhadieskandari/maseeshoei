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
    
    public partial class T_ConsumerProducePrices
    {
        public T_ConsumerProducePrices()
        {
            this.T_BurdenInformations = new HashSet<T_BurdenInformations>();
        }
    
        public int ConsumerProducePriceID { get; set; }
        public int ConsumerID { get; set; }
        public string Date { get; set; }
        public decimal Price { get; set; }
        public bool StateID { get; set; }
        public int ProduceID { get; set; }
    
        public virtual ICollection<T_BurdenInformations> T_BurdenInformations { get; set; }
        public virtual T_Consumers T_Consumers { get; set; }
        public virtual T_Produces T_Produces { get; set; }
        public virtual T_State T_State { get; set; }
    }
}
