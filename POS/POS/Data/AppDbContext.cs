using POS.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POS.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        DbSet<Role> IAppDbContext.Roles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void IAppDbContext.SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
