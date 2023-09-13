using Microsoft.EntityFrameworkCore;
using PhotosForSale.Models;

namespace PhotosForSale.Data
{
    public class ApplicationDbConext : DbContext
    {
        public ApplicationDbConext(DbContextOptions<ApplicationDbConext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 8, CategoryName = "Krajobraz", DisplayOrder = 1 },
                new Category { Id = 9, CategoryName = "Portret", DisplayOrder = 2 },
                new Category { Id = 10, CategoryName = "Natura", DisplayOrder = 3 }
                );
        }
    }
}

