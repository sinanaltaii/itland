//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITLand.Commerce.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public string ProcuctId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal OriginalPrice { get; set; }
        public Nullable<decimal> CampaignPrice { get; set; }
        public Nullable<decimal> DiscountedPrice { get; set; }
    
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
