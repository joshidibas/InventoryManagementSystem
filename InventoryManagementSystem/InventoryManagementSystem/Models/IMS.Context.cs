﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IMSEntities : DbContext
    {
        public IMSEntities()
            : base("name=IMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductSales> ProductSales { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<ProductXDetails> ProductXDetails { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Billing> Billing { get; set; }
        public virtual DbSet<StockManagment> StockManagment { get; set; }
        public virtual DbSet<SupplierDetail> SupplierDetail { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<UserAccounts> UserAccounts { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
    }
}
