using Curriculum.Entities;
using Curriculum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Curriculum.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Change> Changes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.id)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.id)
                .ValueGeneratedOnAdd();
        });
        
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.Property(e => e.id)
                .ValueGeneratedOnAdd();
        });
        
        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.id)
                .ValueGeneratedOnAdd();
        });
        
        modelBuilder.Entity<Change>(entity =>
        {
            entity.Property(e => e.id)
                .ValueGeneratedOnAdd();
        });
    }
}