using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("ProductCategory", Schema = "SalesLT")]
    public partial class SalesltProductcategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductCategoryID")]
        public int ProductCategoryId { get; set; }

        [Column("ParentProductCategoryID")]
        public int? ParentProductCategoryId { get; set; }

        public SalesltProductcategory ProductCategory { get; set; }

        [Required]
        public string Name { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesltProduct> SalesltProducts { get; set; }

        public ICollection<SalesltProductcategory> SalesltProductCategories1 { get; set; }
    }
}