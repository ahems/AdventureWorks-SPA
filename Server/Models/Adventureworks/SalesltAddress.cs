using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("Address", Schema = "SalesLT")]
    public partial class SalesltAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AddressID")]
        public int AddressId { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string StateProvince { get; set; }

        [Required]
        public string CountryRegion { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesltCustomeraddress> SalesltCustomerAddresses { get; set; }

        public ICollection<SalesltSalesorderheader> SalesltSalesOrderHeaders { get; set; }

        public ICollection<SalesltSalesorderheader> SalesltSalesOrderHeaders1 { get; set; }
    }
}