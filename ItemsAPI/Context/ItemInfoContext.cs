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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasData(
                new Item()
                {
                    Id = 1,
                    Name = "Hammer",
                    Description = "Big hammer"
                },
                new Item()
                {
                    Id = 2,
                    Name = "Wrench",
                    Description = "Smaller wrench"
                },
                new Item()
                {
                    Id = 3,
                    Name = "Screwdriver",
                    Description = "Medium screwdriver"

                }
                );

            modelBuilder.Entity<Rating>()
                .HasData(
                new Rating()
                {
                    Id = 1,
                    ItemId = 1,
                    Name = "Id 1, Item 1",
                    Description = "One"
                },
                new Rating()
                {
                    Id = 2,
                    ItemId = 1,
                    Name = "Id 2, Item 1",
                    Description = "Two"
                },
                new Rating()
                {
                    Id = 3,
                    ItemId = 2,
                    Name = "Id 3, Item 2",
                    Description = "Three"
                },
                new Rating()
                {
                    Id = 4,
                    ItemId = 2,
                    Name = "Id 4, Item 2",
                    Description = "Four"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}