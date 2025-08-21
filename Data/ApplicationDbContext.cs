using Microsoft.EntityFrameworkCore;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }

        public DbSet<ServiceBooking> ServiceBooking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Only configure ServiceBooking relationships here

            modelBuilder.Entity<ServiceBooking>()
                .HasOne(sb => sb.Customer)
                .WithMany()
                .HasForeignKey(sb => sb.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceBooking>()
                .HasOne(sb => sb.Vehicle)
                .WithMany()
                .HasForeignKey(sb => sb.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceBooking>()
                .HasOne(sb => sb.ServiceType)
                .WithMany()
                .HasForeignKey(sb => sb.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }


}
