using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("SalesOrderHeader", Schema = "SalesLT")]
    public partial class SalesltSalesorderheader
    {
        [Key]
        [Column("SalesOrderID")]
        public int SalesOrderId { get; set; }

        public byte RevisionNumber { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public byte Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string SalesOrderNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string AccountNumber { get; set; }

        [Column("CustomerID")]
        [Required]
        public int CustomerId { get; set; }

        public SalesltCustomer Customer { get; set; }

        [Column("ShipToAddressID")]
        public int? ShipToAddressId { get; set; }

        public SalesltAddress Address1 { get; set; }

        [Column("BillToAddressID")]
        public int? BillToAddressId { get; set; }

        public SalesltAddress Address { get; set; }

        [Required]
        public string ShipMethod { get; set; }

        public string CreditCardApprovalCode { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalDue { get; set; }

        public string Comment { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesltSalesorderdetail> SalesltSalesOrderDetails { get; set; }
    }
}