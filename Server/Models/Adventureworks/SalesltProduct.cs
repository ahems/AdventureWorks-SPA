using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("Product", Schema = "SalesLT")]
    public partial class SalesltProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductID")]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ProductNumber { get; set; }

        public string Color { get; set; }

        [Required]
        public decimal StandardCost { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        public string Size { get; set; }

        public decimal? Weight { get; set; }

        [Column("ProductCategoryID")]
        public int? ProductCategoryId { get; set; }

        public SalesltProductcategory ProductCategory { get; set; }

        [Column("ProductModelID")]
        public int? ProductModelId { get; set; }

        public SalesltProductmodel ProductModel { get; set; }

        [Required]
        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public byte[] ThumbNailPhoto { get; set; }

        public string ThumbnailPhotoFileName { get; set; }

        [Column("rowguid")]
        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<SalesltSalesorderdetail> SalesltSalesOrderDetails { get; set; }
    }
}