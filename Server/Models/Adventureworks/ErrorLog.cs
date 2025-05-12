using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Server.Models.adventureworks
{
    [Table("ErrorLog", Schema = "dbo")]
    public partial class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ErrorLogID")]
        public int ErrorLogId { get; set; }

        public DateTime ErrorTime { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int ErrorNumber { get; set; }

        public int? ErrorSeverity { get; set; }

        public int? ErrorState { get; set; }

        public string ErrorProcedure { get; set; }

        public int? ErrorLine { get; set; }

        [Required]
        public string ErrorMessage { get; set; }
    }
}