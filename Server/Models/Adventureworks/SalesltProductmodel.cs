using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("ProductModel", Schema = "SalesLT")]
    public partial class SalesltProductmodel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductModelID")]
        public int ProductModelId { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName="xml")]
        public string CatalogDescription { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesltProduct> SalesltProducts { get; set; }

        public ICollection<SalesltProductmodelproductdescription> SalesltProductModelProductDescriptions { get; set; }
    }
}