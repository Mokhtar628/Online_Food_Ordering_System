//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Online_Food_Ordering_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Details
    {
        public int order_id { get; set; }
        public int food_id { get; set; }
        public string Name { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual External_Order External_Order { get; set; }
        public virtual Food_Items Food_Items { get; set; }
    }
}