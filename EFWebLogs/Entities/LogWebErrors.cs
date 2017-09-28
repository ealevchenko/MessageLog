namespace EFWebLogs.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log.LogWebErrors")]
    public partial class LogWebErrors
    {
        public long ID { get; set; }

        public DateTime DateTime { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        public bool? Authentication { get; set; }

        [StringLength(50)]
        public string AuthenticationType { get; set; }

        [StringLength(100)]
        public string UserHostName { get; set; }

        [StringLength(100)]
        public string UserHostAddress { get; set; }

        [StringLength(1000)]
        public string url { get; set; }

        [StringLength(2000)]
        public string PhysicalPath { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(10)]
        public string RequestType { get; set; }

        public int? HttpCode { get; set; }

        public int? HResult { get; set; }

        [StringLength(1000)]
        public string InnerException { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        [StringLength(250)]
        public string Source { get; set; }

        public string StackTrace { get; set; }
    }
}
