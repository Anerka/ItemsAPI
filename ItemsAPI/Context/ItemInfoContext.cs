using ItemsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemsAPI.Context
{
    public class ItemInfoContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Rating> Rating { get; set; }

        public ItemInfoContext(DbContextOptions<ItemInfoContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}