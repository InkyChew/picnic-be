using Microsoft.EntityFrameworkCore;
using picnic_be.Models;

namespace picnic_be.Data
{
    public class PicnicDbContext : DbContext
    {
        public PicnicDbContext()
        {
        }

        public PicnicDbContext(DbContextOptions<PicnicDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanFood> PlanFoods { get; set; }
        public DbSet<PlanTool> PlanTools { get; set; }
        public DbSet<PlanUser> PlanUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanUser>()
                .HasKey(pu => new { pu.PlanId, pu.UserId });

            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Foods)
                .WithOne(f => f.Plan)
                .HasForeignKey(f => f.PlanId);

            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Tools)
                .WithOne(t => t.Plan)
                .HasForeignKey(t => t.PlanId);

            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Users)
                .WithOne(u => u.Plan)
                .HasForeignKey(u => u.PlanId);

            modelBuilder.Entity<PlanFood>()
                .HasMany(p => p.Preparers)
                .WithMany(u => u.Foods)
                .UsingEntity("FoodPreparers");

            modelBuilder.Entity<PlanTool>()
                .HasMany(p => p.Preparers)
                .WithMany(u => u.Tools)
                .UsingEntity("ToolPreparers");
        }
    }
}
