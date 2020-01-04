using mvctk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvctk.DAL
{
    public class CourseDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<course>().ToTable("tblCourses");
        }

        public DbSet<course> Courses { get; set; }
    }
}