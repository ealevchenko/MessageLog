namespace EFLogs.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log.LogErrors")]
    public partial class LogErrors
    {
        public long ID { get; set; }

        public DateTime DateTime { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string UserHostName { get; set; }

        [StringLength(100)]
        public string UserHostAddress { get; set; }

        [StringLength(2000)]
        public string PhysicalPath { get; set; }

        [StringLength(2000)]
        public string UserMessage { get; set; }

        public int? Service { get; set; }

        public int? EventID { get; set; }

        public int? HResult { get; set; }

        [StringLength(2000)]
        public string InnerException { get; set; }

        [StringLength(2000)]
        public string Message { get; set; }

        [StringLength(250)]
        public string Source { get; set; }

        public string StackTrace { get; set; }
    }
}
