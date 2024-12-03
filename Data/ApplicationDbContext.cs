using AQIViewer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AQIViewer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<AirQualityRecord> AirQualityRecords { get; set; }
        public DbSet<Alert> Alert { get; set; }
        public DbSet<AQRForecast> AQRForecast { get; set; }
        public DbSet<Forecast> Forecast { get; set; }
        public DbSet<LocationPoint> LocationPoint { get; set; }
        public DbSet<Pollutant> Pollutant { get; set; }
        public DbSet<PollutantLevel> PollutantLevel { get; set; }
        public DbSet<PollutionLevel> PollutionLevel { get; set; }
        public DbSet<PollutionRecord> PollutionRecord { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserLocation> UserLocation { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // PollutionLevel -> PollutantLevel
            base.OnModelCreating(builder);
            builder.Entity<PollutionLevel>()
              .HasMany(pl => pl.PollutantLevels)
              .WithOne(pl => pl.PollutionLevel)
              .HasForeignKey(pl => pl.PollutionLevelId)
              .IsRequired();

            // Pollutant -> PollutantLevel
            base.OnModelCreating(builder);
            builder.Entity<Pollutant>()
              .HasMany(p => p.PollutantLevels)
              .WithOne(pl => pl.Pollutant)
              .HasForeignKey(pl => pl.PollutantId)
              .IsRequired();

            // Pollutant -> PollutionRecord
            base.OnModelCreating(builder);
            builder.Entity<Pollutant>()
              .HasMany(p => p.PollutionRecords)
              .WithOne(pr => pr.Pollutant)
              .HasForeignKey(pr => pr.PollutantId)
              .IsRequired();

            // AirQualityRecord -> PollutionRecord
            base.OnModelCreating(builder);
            builder.Entity<AirQualityRecord>()
              .HasMany(aqr => aqr.PollutionRecords)
              .WithOne(pr => pr.AirQualityRecord)
              .HasForeignKey(pr => pr.AQRId)
              .IsRequired();

            // AirQualityRecord -> Alert
            base.OnModelCreating(builder);
            builder.Entity<AirQualityRecord>()
              .HasMany(aqr => aqr.Alerts)
              .WithOne(a => a.AirQualityRecord)
              .HasForeignKey(a => a.AQRId)
              .IsRequired();

            // AirQualityRecord -> AQRForecast
            base.OnModelCreating(builder);
            builder.Entity<AirQualityRecord>()
              .HasMany(aqr => aqr.AQRForecasts)
              .WithOne(af => af.AirQualityRecord)
              .HasForeignKey(af => af.AQRId)
              .IsRequired();

            // Forecast -> AQRForecast
            base.OnModelCreating(builder);
            builder.Entity<Forecast>()
              .HasMany(f => f.AQRForecasts)
              .WithOne(af => af.Forecast)
              .HasForeignKey(af => af.ForecastId)
              .IsRequired();

            // LocationPoint -> AirQualityRecord
            base.OnModelCreating(builder);
            builder.Entity<LocationPoint>()
              .HasMany(lp => lp.AirQualityRecords)
              .WithOne(aqr => aqr.LocationPoint)
              .HasForeignKey(aqr => aqr.LocationPointId)
              .IsRequired();

            // LocationPoint -> UserLocation
            base.OnModelCreating(builder);
            builder.Entity<LocationPoint>()
              .HasMany(lp => lp.UserLocations)
              .WithOne(ul => ul.LocationPoint)
              .HasForeignKey(ul => ul.LocationPointId)
              .IsRequired();

            // User -> UserLocation
            base.OnModelCreating(builder);
            builder.Entity<User>()
              .HasMany(u => u.UserLocations)
              .WithOne(ul => ul.User)
              .HasForeignKey(ul => ul.UserId)
              .IsRequired();

            // User -> UserAlert
            base.OnModelCreating(builder);
            builder.Entity<User>()
              .HasMany(u => u.UserAlerts)
              .WithOne(ua => ua.User)
              .HasForeignKey(ua => ua.UserId)
              .IsRequired();

            // Alert -> UserAlert
            base.OnModelCreating(builder);
            builder.Entity<Alert>()
              .HasMany(a => a.UserAlerts)
              .WithOne(ua => ua.Alert)
              .HasForeignKey(ua => ua.AlertId)
              .IsRequired();

            builder.Entity<User>().ToTable("AspNetUsers");

            //    builder.Entity<ProjectUser>()
            //        .HasOne(pu => pu.Project)
            //        .WithMany(p => p.ProjectUsers)
            //        .HasForeignKey(pu => pu.ProjectId);

            //    builder.Entity<ProjectUser>()
            //        .HasOne(pu => pu.User)
            //        .WithMany(au => au.ProjectUsers)
            //        .HasForeignKey(pu => pu.UserId);

        }
        public DbSet<AQIViewer.Models.UserAlert> UserAlert { get; set; } = default!;

    }
}
