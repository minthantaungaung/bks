using bks.domain.Entities;
using BKS.Models;
using Microsoft.EntityFrameworkCore;

namespace BKS.Models
{
	public class BookingDbContext : DbContext
	{
		public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Package> Packages { get; set; }
		public DbSet<ClassSchedule> ClassSchedules { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Waitlist> Waitlists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Booking>()
				.HasOne(b => b.User)
				.WithMany(u => u.Bookings)
				.HasForeignKey(b => b.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Booking>()
				.HasOne(b => b.Schedule)
				.WithMany(s => s.Bookings)
				.HasForeignKey(b => b.ClassScheduleId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Waitlist>()
				.HasOne(w => w.User)
				.WithMany(u => u.Waitlists)
				.HasForeignKey(w => w.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Waitlist>()
				.HasOne(w => w.Schedule)
				.WithMany(s => s.Waitlists)
				.HasForeignKey(w => w.ScheduleId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}