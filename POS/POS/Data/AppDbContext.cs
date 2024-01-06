using POS.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Role> role { get; set; }

    }
}
