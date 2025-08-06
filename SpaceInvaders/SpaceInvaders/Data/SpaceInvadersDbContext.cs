using Microsoft.EntityFrameworkCore;
using SpaceInvaders.Models;
using SpaceInvaders.Models.Aliens;

namespace SpaceInvaders.Data
{
    public class SpaceInvadersDbContext : DbContext
    {
        public SpaceInvadersDbContext(DbContextOptions<SpaceInvadersDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Alien> Aliens { get; set; }
        public DbSet<AlienType1> AlienType1s { get; set; }
        public DbSet<AlienType2> AlienType2s { get; set; }
        public DbSet<AlienType3> AlienType3s { get; set; }
        public DbSet<AlienType4> AlienType4s { get; set; }
        public DbSet<Projectile> Projectiles { get; set; }
        public DbSet<Shield> Shields { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship between Player and Weapon
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Weapon)
                .WithOne(w => w.Player)
                .HasForeignKey<Weapon>(w => w.PlayerId);

            // Configure one-to-many relationship between Player and Projectile
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Projectiles)
                .WithOne(pr => pr.Player)
                .HasForeignKey(pr => pr.PlayerId);

            // Configure one-to-many relationship between Player and Score
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Scores)
                .WithOne(s => s.Player)
                .HasForeignKey(s => s.PlayerId);
        }
    }
}
