using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("ProductModelProductDescription", Schema = "SalesLT")]
    public partial class SalesltProductmodelproductdescription
    {
        [Key]
        [Column("ProductModelID")]
        [Required]
        public int ProductModelId { get; set; }

        public SalesltProductmodel ProductModel { get; set; }

        [Key]
        [Column("ProductDescriptionID")]
        [Required]
        public int ProductDescriptionId { get; set; }

        public SalesltProductdescription ProductDescription { get; set; }

        [Key]
        [Required]
        public string Culture { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}