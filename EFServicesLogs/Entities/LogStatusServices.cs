using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFServicesLogs.Entities
{

    [Table("Log.LogStatusServices")]
    public partial class LogStatusServices
    {
        [Key]
        public int id { get; set; }

        public int service { get; set; }

        public DateTime? start { get; set; }

        public DateTime? execution { get; set; }

        public int? current { get; set; }

        public int? max { get; set; }

        public int? min { get; set; }

    }
}
