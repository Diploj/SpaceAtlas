using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.DataAccess;

public class SpaceAtlasDbContext : IdentityDbContext<UserEntity,IdentityRole<Guid>, Guid>
{
    //public DbSet<UserEntity> Users { get; set; }
    public DbSet<StarEntity> Stars { get; set; }
    public DbSet<PlanetEntity> Planets { get; set; }
    
    public SpaceAtlasDbContext(DbContextOptions<SpaceAtlasDbContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.UserName).IsUnique();
            ;
        
        modelBuilder.Entity<StarEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<StarEntity>().HasOne(x => x.User)
            .WithMany(x=>x.Stars)
            .HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<PlanetEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<PlanetEntity>().HasOne(x => x.User)
            .WithMany(x=>x.Planets)
            .HasForeignKey(x => x.UserId);
        
    }
    
    
}