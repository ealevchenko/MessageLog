namespace EFWebLogs.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log.LogWebVisit")]
    public partial class LogWebVisit
    {
        public long ID { get; set; }

        public DateTime DateTime { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        public bool? Authentication { get; set; }

        [StringLength(50)]
        public string AuthenticationType { get; set; }

        [StringLength(100)]
        public string MachineName { get; set; }

        [StringLength(100)]
        public string MachineIP { get; set; }

        [StringLength(1000)]
        public string url { get; set; }

        [StringLength(2000)]
        public string PhysicalPath { get; set; }

        [StringLength(100)]
        public string ActionName { get; set; }

        [StringLength(100)]
        public string ControllerName { get; set; }

        [StringLength(1000)]
        public string RolesAccess { get; set; }

        public bool? Access { get; set; }
    }
}
