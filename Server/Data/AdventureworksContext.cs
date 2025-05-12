using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Server.Models.adventureworks;

namespace AdventureWorks.Server.Data
{
    public partial class adventureworksContext : DbContext
    {
        public adventureworksContext()
        {
        }

        public adventureworksContext(DbContextOptions<adventureworksContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>().HasKey(table => new {
                table.CustomerId, table.AddressId
            });

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>().HasKey(table => new {
                table.ProductModelId, table.ProductDescriptionId, table.Culture
            });

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>().HasKey(table => new {
                table.SalesOrderId, table.SalesOrderDetailId
            });

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>()
              .HasOne(i => i.Address)
              .WithMany(i => i.SalesltCustomerAddresses)
              .HasForeignKey(i => i.AddressId)
              .HasPrincipalKey(i => i.AddressId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>()
              .HasOne(i => i.Customer)
              .WithMany(i => i.SalesltCustomerAddresses)
              .HasForeignKey(i => i.CustomerId)
              .HasPrincipalKey(i => i.CustomerId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProduct>()
              .HasOne(i => i.ProductCategory)
              .WithMany(i => i.SalesltProducts)
              .HasForeignKey(i => i.ProductCategoryId)
              .HasPrincipalKey(i => i.ProductCategoryId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProduct>()
              .HasOne(i => i.ProductModel)
              .WithMany(i => i.SalesltProducts)
              .HasForeignKey(i => i.ProductModelId)
              .HasPrincipalKey(i => i.ProductModelId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>()
              .HasOne(i => i.ProductCategory)
              .WithMany(i => i.SalesltProductCategories1)
              .HasForeignKey(i => i.ParentProductCategoryId)
              .HasPrincipalKey(i => i.ProductCategoryId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>()
              .HasOne(i => i.ProductDescription)
              .WithMany(i => i.SalesltProductModelProductDescriptions)
              .HasForeignKey(i => i.ProductDescriptionId)
              .HasPrincipalKey(i => i.ProductDescriptionId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>()
              .HasOne(i => i.ProductModel)
              .WithMany(i => i.SalesltProductModelProductDescriptions)
              .HasForeignKey(i => i.ProductModelId)
              .HasPrincipalKey(i => i.ProductModelId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>()
              .HasOne(i => i.Product)
              .WithMany(i => i.SalesltSalesOrderDetails)
              .HasForeignKey(i => i.ProductId)
              .HasPrincipalKey(i => i.ProductId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>()
              .HasOne(i => i.SalesOrderHeader)
              .WithMany(i => i.SalesltSalesOrderDetails)
              .HasForeignKey(i => i.SalesOrderId)
              .HasPrincipalKey(i => i.SalesOrderId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .HasOne(i => i.Address)
              .WithMany(i => i.SalesltSalesOrderHeaders)
              .HasForeignKey(i => i.BillToAddressId)
              .HasPrincipalKey(i => i.AddressId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .HasOne(i => i.Customer)
              .WithMany(i => i.SalesltSalesOrderHeaders)
              .HasForeignKey(i => i.CustomerId)
              .HasPrincipalKey(i => i.CustomerId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .HasOne(i => i.Address1)
              .WithMany(i => i.SalesltSalesOrderHeaders1)
              .HasForeignKey(i => i.ShipToAddressId)
              .HasPrincipalKey(i => i.AddressId);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>()
              .Property(p => p.LineTotal)
              .HasComputedColumnSql(@"(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))")
              .ValueGeneratedOnAddOrUpdate()
              .Metadata.SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .Property(p => p.SalesOrderNumber)
              .HasComputedColumnSql(@"(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID],(0)),N'*** ERROR ***'))")
              .ValueGeneratedOnAddOrUpdate()
              .Metadata.SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .Property(p => p.TotalDue)
              .HasComputedColumnSql(@"(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))")
              .ValueGeneratedOnAddOrUpdate()
              .Metadata.SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltAddress>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.BuildVersion>()
              .Property(p => p.VersionDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.BuildVersion>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltCustomer>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.ErrorLog>()
              .Property(p => p.ErrorTime)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProduct>()
              .Property(p => p.SellStartDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProduct>()
              .Property(p => p.SellEndDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProduct>()
              .Property(p => p.DiscontinuedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProduct>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .Property(p => p.OrderDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .Property(p => p.DueDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .Property(p => p.ShipDate)
              .HasColumnType("datetime");

            builder.Entity<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader>()
              .Property(p => p.ModifiedDate)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltAddress> SalesltAddresses { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.BuildVersion> BuildVersions { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltCustomer> SalesltCustomers { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltCustomeraddress> SalesltCustomerAddresses { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.ErrorLog> ErrorLogs { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltProduct> SalesltProducts { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltProductcategory> SalesltProductCategories { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltProductdescription> SalesltProductDescriptions { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltProductmodel> SalesltProductModels { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltProductmodelproductdescription> SalesltProductModelProductDescriptions { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderdetail> SalesltSalesOrderDetails { get; set; }

        public DbSet<AdventureWorks.Server.Models.adventureworks.SalesltSalesorderheader> SalesltSalesOrderHeaders { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}