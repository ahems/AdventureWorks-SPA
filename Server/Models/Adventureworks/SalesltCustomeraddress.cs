using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("CustomerAddress", Schema = "SalesLT")]
    public partial class SalesltCustomeraddress
    {
        [Key]
        [Column("CustomerID")]
        [Required]
        public int CustomerId { get; set; }

        public SalesltCustomer Customer { get; set; }

        [Key]
        [Column("AddressID")]
        [Required]
        public int AddressId { get; set; }

        public SalesltAddress Address { get; set; }

        [Required]
        public string AddressType { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}