﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BistroDriveWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BistroDriveEntities : DbContext
    {
        public BistroDriveEntities()
            : base("name=BistroDriveEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aspnetrole> aspnetroles { get; set; }
        public virtual DbSet<aspnetuserclaim> aspnetuserclaims { get; set; }
        public virtual DbSet<aspnetuserlogin> aspnetuserlogins { get; set; }
        public virtual DbSet<aspnetuser> aspnetusers { get; set; }
        public virtual DbSet<chatmessage> chatmessages { get; set; }
        public virtual DbSet<dish> dishes { get; set; }
        public virtual DbSet<dishreview> dishreviews { get; set; }
        public virtual DbSet<dishtype> dishtypes { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<ordercontactmethod> ordercontactmethods { get; set; }
        public virtual DbSet<orderdelivery> orderdeliveries { get; set; }
        public virtual DbSet<orderingridientbuyer> orderingridientbuyers { get; set; }
        public virtual DbSet<orderpaymentmethod> orderpaymentmethods { get; set; }
        public virtual DbSet<orderproduct> orderproducts { get; set; }
        public virtual DbSet<orderstatu> orderstatus { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<userdescription> userdescriptions { get; set; }
        public virtual DbSet<usertoken> usertokens { get; set; }
    }
}
