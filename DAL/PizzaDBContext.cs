using Domain.ConnectModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)

        {

        }

        public DbSet<PizzaType>? PizzaTypes { get; set; }
        public DbSet<Lisa>? Lisas { get; set; }
        public DbSet<POrder>? POrders { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<PizzaTypeLisaAssignment>? PizzaTypeLisaAssignments { get; set; }
        public DbSet<POrderLisaAssignment>? POrderLisaAssignments { get; set; }
        public DbSet<POrderOrderAssignment>? POrderOrderAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PizzaType>().ToTable("PizzaTypes");

            modelBuilder.Entity<Lisa>().ToTable("Lisas");

            modelBuilder.Entity<POrder>().ToTable("POrders");

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<PizzaTypeLisaAssignment>().ToTable("PizzaTypeLisaAssignments");
            modelBuilder.Entity<POrderLisaAssignment>().ToTable("POrderLisaAssignments");
            modelBuilder.Entity<POrderOrderAssignment>().ToTable("POrderOrderAssignments");
        }
    }
}
