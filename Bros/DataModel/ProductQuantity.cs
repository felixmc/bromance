//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bros.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductQuantity
    {
        public ProductQuantity()
        {
            this.ShoppingCarts = new HashSet<ShoppingCart>();
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}