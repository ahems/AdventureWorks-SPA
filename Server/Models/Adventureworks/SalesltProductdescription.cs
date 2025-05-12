using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("ProductDescription", Schema = "SalesLT")]
    public partial class SalesltProductdescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductDescriptionID")]
        public int ProductDescriptionId { get; set; }

        [Required]
        public string Description { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesltProductmodelproductdescription> SalesltProductModelProductDescriptions { get; set; }
    }
}