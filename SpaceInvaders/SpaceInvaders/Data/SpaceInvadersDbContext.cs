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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelo, se necessário
            // Exemplo: modelBuilder.Entity<Player>().HasKey(p => p.Id);
        }
    }
}
