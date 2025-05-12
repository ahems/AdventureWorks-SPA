using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("Customer", Schema = "SalesLT")]
    public partial class SalesltCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CustomerID")]
        public int CustomerId { get; set; }

        public bool NameStyle { get; set; }

        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string CompanyName { get; set; }

        public string SalesPerson { get; set; }

        public string EmailAddress { get; set; }

        public string Phone { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesltCustomeraddress> SalesltCustomerAddresses { get; set; }

        public ICollection<SalesltSalesorderheader> SalesltSalesOrderHeaders { get; set; }
    }
}