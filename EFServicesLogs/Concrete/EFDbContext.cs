using EFServicesLogs.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFServicesLogs.Concrete
{
    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=LOG")
        {
        }

        public virtual DbSet<LogServices> LogServices { get; set; }
        public virtual DbSet<LogStatusServices> LogStatusServices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
