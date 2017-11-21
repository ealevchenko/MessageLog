namespace EFServicesLogs.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log.LogServices")]
    public partial class LogServices
    {
        [Key]
        public long id { get; set; }

        public int service { get; set; }

        public DateTime start { get; set; }

        public int duration { get; set; }

        public int? code_return { get; set; }
    }
}
