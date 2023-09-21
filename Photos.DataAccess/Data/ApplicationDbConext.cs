using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Photos.Models.Models;

namespace Photos.DataAccess.Data
{
    public class ApplicationDbConext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbConext(DbContextOptions<ApplicationDbConext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MyPhoto> MyPhotos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Category>().HasData(
            //    new Category { Id = 8, CategoryName = "Krajobraz", DisplayOrder = 1 },
            //    new Category { Id = 9, CategoryName = "Portret", DisplayOrder = 2 },
            //    new Category { Id = 10, CategoryName = "Natura", DisplayOrder = 3 }
            //    );

            //modelBuilder.Entity<MyPhoto>().HasData(
            //    new MyPhoto
            //    {
            //        Id = 1,
            //        Title = "Fortune of Time",
            //        Author = "Billy Spark",
            //        Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            //        Price = 90,
            //        CategoryId = 13,
            //        ImageUrl = ""
            //    },
            //    new MyPhoto
            //    {
            //        Id = 2,
            //        Title = "Dark Skies",
            //        Author = "Nancy Hoover",
            //        Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            //        Price = 30,
            //        CategoryId = 13,
            //        ImageUrl = ""
            //    },
            //    new MyPhoto
            //    {
            //        Id = 3,
            //        Title = "Vanish in the Sunset",
            //        Author = "Julian Button",
            //        Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            //        Price = 50,
            //        CategoryId = 13,
            //        ImageUrl = ""
            //    },
            //    new MyPhoto
            //    {
            //        Id = 4,
            //        Title = "Cotton Candy",
            //        Author = "Abby Muscles",
            //        Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            //        Price = 65,
            //        CategoryId = 13,
            //        ImageUrl = ""
            //    },
            //    new MyPhoto
            //    {
            //        Id = 5,
            //        Title = "Rock in the Ocean",
            //        Author = "Ron Parker",
            //        Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            //        Price = 27,
            //        CategoryId = 13,
            //        ImageUrl = ""
            //    },
            //    new MyPhoto
            //    {
            //        Id = 6,
            //        Title = "Leaves and Wonders",
            //        Author = "Laura Phantom",
            //        Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            //        Price = 23,
            //        CategoryId = 13,
            //        ImageUrl = ""
            //    });
        }
    }
}

