﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TesTransaction.Data.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestTransactionEntities : DbContext
    {
        public TestTransactionEntities()
            : base("name=TestTransactionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AGE> AGEs { get; set; }
        public virtual DbSet<BRAND> BRANDs { get; set; }
        public virtual DbSet<CASH_BOTTOM_DAY> CASH_BOTTOM_DAYs { get; set; }
        public virtual DbSet<CATEGORY> CATEGORYs { get; set; }
        public virtual DbSet<HERO> HEROs { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<PAYMENT_METHOD> PAYMENT_METHODs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<TERMINAL> TERMINALs { get; set; }
        public virtual DbSet<TICKET> TICKETs { get; set; }
        public virtual DbSet<TRANSACTION_DETAILS> TRANSACTION_DETAILSs { get; set; }
        public virtual DbSet<TRANSACTIONS> TRANSACTIONSs { get; set; }
        public virtual DbSet<VAT> VATs { get; set; }
    }
}