using Microsoft.EntityFrameworkCore;

namespace Mission08_Team3_7.Models;

public class TimeMangementContext : DbContext
{
    // this will go into the contrller.cs file for the options...
    public TimeMangementContext(DbContextOptions<TimeMangementContext> options) : base(options) // constructor
    {
    }
    // this is where you name your table in the database
    public DbSet<Category> Categories { get; set; }
    public DbSet<Task> Tasks { get; set; }

    
    // this is how you can "seed the data" basically add data to the database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>().HasData(
            new Task { CategoryId = 1, CategoryName = "Home"}, 
            new Task { CategoryId = 2, CategoryName = "School"},
            new Task { CategoryId = 3, CategoryName = "Work"},
            new Task { CategoryId = 4, CategoryName = "Church"}
                
        );
    }

}


