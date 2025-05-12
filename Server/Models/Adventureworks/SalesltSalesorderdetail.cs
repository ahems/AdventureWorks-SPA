using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("SalesOrderDetail", Schema = "SalesLT")]
    public partial class SalesltSalesorderdetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SalesOrderID")]
        [Required]
        public int SalesOrderId { get; set; }

        public SalesltSalesorderheader SalesOrderHeader { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SalesOrderDetailID")]
        public int SalesOrderDetailId { get; set; }

        [Required]
        public short OrderQty { get; set; }

        [Column("ProductID")]
        [Required]
        public int ProductId { get; set; }

        public SalesltProduct Product { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal LineTotal { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}