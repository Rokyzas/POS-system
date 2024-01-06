using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace POS.Data
{
    public interface IAppDbContext
    {
        DbSet<Role> Roles { get; set; }

        void SaveChanges();
    }
}