using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Travel> Travelies { get; set; }
        public DbSet<ActivityAttendee> ActivityAtendees { get; set; }
        public DbSet<TravelAttendee> TravelAtendees { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new {aa.AppUserId, aa.ActivityId}));

            builder.Entity<TravelAttendee>(x => x.HasKey(aa => new {aa.AppUserId, aa.TravelId}));

            builder.Entity<ActivityAttendee>()
            .HasOne(u => u.AppUser)
            .WithMany(a => a.Activities)
            .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<ActivityAttendee>()
            .HasOne(u => u.Acitivity)
            .WithMany(a => a.Attendees)
            .HasForeignKey(aa => aa.ActivityId);

            builder.Entity<TravelAttendee>()
            .HasOne(u => u.AppUser)
            .WithMany(a => a.Travelies)
            .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<TravelAttendee>()
            .HasOne(u => u.Travel)
            .WithMany(a => a.Attendees)
            .HasForeignKey(aa => aa.TravelId);

        }

    }
}