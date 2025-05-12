using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("BuildVersion", Schema = "dbo")]
    public partial class BuildVersion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SystemInformationID")]
        public byte SystemInformationId { get; set; }

        [Column("Database Version")]
        [Required]
        public string DatabaseVersion { get; set; }

        [Required]
        public DateTime VersionDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}