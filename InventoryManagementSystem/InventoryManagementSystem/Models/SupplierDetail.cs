//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplierDetail
    {
        public int SupplierDetailID { get; set; }
        public int SupplierID { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string ContactPersonName { get; set; }
    
        public virtual Suppliers Suppliers { get; set; }
    }
}
