using Microsoft.EntityFrameworkCore;
using WebApiJwtAuth.Models;

namespace WebApiJwtAuth.Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options)
            :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
