using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Mapping;

namespace TrackingProject.Data.Context
{
    public class TrackingProjectContext : DbContext
    {
        public TrackingProjectContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> _User { get; set; }
        public DbSet<Firm> _Firm { get; set; }
        public DbSet<Student> _Student { get; set; }
        public DbSet<Route> _Route { get; set; }
        public DbSet<RouteLine> _RouteLine { get; set; }
        public DbSet<RouteMovement> _RouteMovement { get; set; }
        public DbSet<RouteStudent> _RouteStudent { get; set; }
        public DbSet<Parent> _Parent { get; set; }
        public DbSet<ParentStudent> _ParentStudent { get; set; }
        public DbSet<Driver> _Driver { get; set; }
        public DbSet<Movement> _Movement { get; set; }
        public DbSet<Vehicle> _Vehicle { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DriverMap());
            modelBuilder.Configurations.Add(new FirmMap());
            modelBuilder.Configurations.Add(new ParentMap());
            modelBuilder.Configurations.Add(new ParentStudentMap());
            modelBuilder.Configurations.Add(new RouteMap());
            modelBuilder.Configurations.Add(new RouteMovementMap());
            modelBuilder.Configurations.Add(new RouteLineMap());
            modelBuilder.Configurations.Add(new RouteStudentMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new MovementMap());
            modelBuilder.Configurations.Add(new VehicleMap());


            base.OnModelCreating(modelBuilder);
        }
    }
}
