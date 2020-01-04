namespace mvctk.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SQLContext : DbContext
    {
        public SQLContext()
            : base("name=SQLContext")
        {
        }

        public virtual DbSet<course> courses { get; set; }
        public virtual DbSet<grade> grades { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
