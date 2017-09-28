namespace EFWebLogs.Concrete
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EFWebLogs.Entities;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=LOG")
        {
        }

        public virtual DbSet<LogWebErrors> LogWebErrors { get; set; }
        public virtual DbSet<LogWebVisit> LogWebVisit { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
