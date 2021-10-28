using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using Vehicle.Tracking.Entities.Enums;
using Vehicle.Tracking.Entities.Models;

namespace Vehicle.Tracking.DataAccess.Concrete.EntityFramework.Contexts
{


    public class VehicleTrackDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public VehicleTrackDbContext(DbContextOptions<VehicleTrackDbContext> options, IConfiguration configuration)
                                                                                : base(options)
        {
            Configuration = configuration;
            
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected VehicleTrackDbContext(DbContextOptions options, IConfiguration configuration)
                                                                        : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<Entities.Models.Vehicle> Vehicles { get; set; }
        public DbSet<LocationHistory> LocationHistories { get; set; }


        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #region seed user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    AddedBy = 1,
                    AddedDate = DateTime.Now,
                    Status = (int)StatusType.Active,
                    Name = "Test Admin",
                    Surname="Test",
                    Email = "testadmin@test.com",
                    Password = "testpwd!",
                    Id = 1
                });
            modelBuilder.Entity<User>().HasData(
             new User
             {
                 AddedBy = 1,
                 AddedDate = DateTime.Now,
                 Status = (int)StatusType.Active,
                 Name = "Test Customer",
                 Surname = "Test",
                 Email = "testcustomer@test.com",
                 Password = "testcustomerpwd!",
                 Id = 2
             });
            #endregion

            #region seed role and right
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    AddedBy = 1,
                    AddedDate = DateTime.Now,
                    Status = (int)StatusType.Active,
                    Name = "Admin",
                    Id = 1
                });
            modelBuilder.Entity<Role>().HasData(
            new Role
            {
                AddedBy = 1,
                AddedDate = DateTime.Now,
                Status = (int)StatusType.Active,
                Name = "Customer",
                Id = 2
            });
            modelBuilder.Entity<Right>().HasData(
                new Right
                {
                    UserId = 1,
                    AddedDate = DateTime.Now,
                    Status = (int)StatusType.Active,
                    RoleId = 1,
                    AddedBy = 1,
                    Id=1
                });
            modelBuilder.Entity<Right>().HasData(
              new Right
              {
                  UserId = 2,
                  AddedDate = DateTime.Now,
                  Status = (int)StatusType.Active,
                  RoleId = 2,
                  AddedBy = 1,
                  Id = 2
              });
            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("VehicleTrackMsqlContext")).EnableSensitiveDataLogging());

            }
        }

    }
}
