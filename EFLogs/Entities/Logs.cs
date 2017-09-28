namespace EFLogs.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log.Logs")]
    public partial class Logs
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

        public int? Service { get; set; }

        public int? EventID { get; set; }

        public int? Level { get; set; }

        public string Log { get; set; }
    }
}
