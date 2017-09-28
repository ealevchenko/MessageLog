namespace EFLogs.Concrete
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EFLogs.Entities;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=Log")
        {
        }

        public virtual DbSet<LogErrors> LogErrors { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
