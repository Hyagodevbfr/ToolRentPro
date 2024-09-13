using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using ToolRentPro.API.Model.Categories;
using ToolRentPro.API.Model.Maintenances;
using ToolRentPro.API.Model.Tool;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Infra;

public class AppDbContext : IdentityDbContext<UserModel>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<ToolModel>? Tools { get; set; }
    public DbSet<Maintenance>? Maintenances { get; set; }
    public DbSet<Category>? Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ToolModel>( )
            .ToTable("Tools");

        builder.Entity<Maintenance>( )
            .ToTable("Maintenances");

        builder.Entity<Category>( )
            .ToTable("Categories");

        builder.Entity<Maintenance>( )
            .HasOne(m => m.Tool)
            .WithMany(t => t.MaintenanceHistory)
            .HasForeignKey(m => m.ToolId);

        builder.Entity<ToolModel>( )
            .HasOne(t => t.Category)
            .WithMany( )
            .HasForeignKey(t => t.CategoryId);
    }
}
