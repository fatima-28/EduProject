using Edu.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Edu.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImg> CourseImgs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
