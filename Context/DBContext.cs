using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> ProductEntities { get; set; }
    }
}
