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
    
    public partial class T_State
    {
        public T_State()
        {
            this.T_ConsumerProducePrices = new HashSet<T_ConsumerProducePrices>();
            this.T_Consumers = new HashSet<T_Consumers>();
            this.T_ProducerProducePrices = new HashSet<T_ProducerProducePrices>();
            this.T_Producers = new HashSet<T_Producers>();
            this.T_TransportPrices = new HashSet<T_TransportPrices>();
            this.T_Trucks = new HashSet<T_Trucks>();
        }
    
        public bool StateID { get; set; }
        public string StateName { get; set; }
    
        public virtual ICollection<T_ConsumerProducePrices> T_ConsumerProducePrices { get; set; }
        public virtual ICollection<T_Consumers> T_Consumers { get; set; }
        public virtual ICollection<T_ProducerProducePrices> T_ProducerProducePrices { get; set; }
        public virtual ICollection<T_Producers> T_Producers { get; set; }
        public virtual ICollection<T_TransportPrices> T_TransportPrices { get; set; }
        public virtual ICollection<T_Trucks> T_Trucks { get; set; }
    }
}
