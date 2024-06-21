using Microsoft.EntityFrameworkCore;
using QRCulturalBackEnd.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace QRCulturalBackEnd.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Monumento> Monumentos { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Monumento>()
                .HasKey(m => m.id);

            modelBuilder.Entity<Card>()
                .HasKey(c => c.id);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.monumento)
                .WithOne(m => m.card)
                .HasForeignKey<Card>(c => c.monumentoId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Admin>()
                .HasKey(a => a.usuario);

            base.OnModelCreating(modelBuilder);
        }

    }
}
