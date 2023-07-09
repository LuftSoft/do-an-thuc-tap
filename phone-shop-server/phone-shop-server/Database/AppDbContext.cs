using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach(var entityType in builder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    tableName = tableName.Substring(6);
                    entityType.SetTableName(tableName);
                }
            }
        }
    }
}
