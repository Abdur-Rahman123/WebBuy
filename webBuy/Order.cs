//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webBuy
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int orderId { get; set; }
        public Nullable<double> total { get; set; }
        public int paymentId { get; set; }
        public string date { get; set; }
    }
}