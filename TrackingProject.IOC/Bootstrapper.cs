using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Data.Repository;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Service.DriverServices;
using TrackingProject.Service.FirmServices;
using TrackingProject.Service.ParentServices;
using TrackingProject.Service.RouteServices;
using TrackingProject.Service.StudentServices;
using TrackingProject.Service.UserServices;
using TrackingProject.Service.VehicleServices;
using TrackingProject.Service.MovementServices;

namespace TrackingProject.IOC
{
    public static class IOCExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }

        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }

    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
             
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.BindInRequestScope<IGenericRepository<Driver>, GenericRepository<Driver>>();
            container.BindInRequestScope<IGenericRepository<Firm>, GenericRepository<Firm>>();
            container.BindInRequestScope<IGenericRepository<Parent>, GenericRepository<Parent>>();
            container.BindInRequestScope<IGenericRepository<ParentStudent>, GenericRepository<ParentStudent>>();
            container.BindInRequestScope<IGenericRepository<Route>, GenericRepository<Route>>();
            container.BindInRequestScope<IGenericRepository<RouteMovement>, GenericRepository<RouteMovement>>();
            container.BindInRequestScope<IGenericRepository<RouteLine>, GenericRepository<RouteLine>>();
            container.BindInRequestScope<IGenericRepository<RouteMovement>, GenericRepository<RouteMovement>>();
            container.BindInRequestScope<IGenericRepository<Student>, GenericRepository<Student>>();
            container.BindInRequestScope<IGenericRepository<User>, GenericRepository<User>>();
            container.BindInRequestScope<IGenericRepository<Vehicle>, GenericRepository<Vehicle>>();
            container.BindInRequestScope<IGenericRepository<Movement>, GenericRepository<Movement>>();


            container.BindInRequestScope<IUnitOfWork, UnitOfWork>();

            container.BindInRequestScope<IDriverService, DriverService>();
            container.BindInRequestScope<IFirmService, FirmService>();
            container.BindInRequestScope<IParentService, ParentService>();
            container.BindInRequestScope<IParentStudentService, ParentStudentService>();
            container.BindInRequestScope<IRouteService, RouteService>();
            container.BindInRequestScope<IRouteMovementService, RouteMovementService>();
            container.BindInRequestScope<IRouteLineService, RouteLineService>();
            container.BindInRequestScope<IRouteStudentService, RouteStudentService>();
            container.BindInRequestScope<IStudentService, StudentService>();
            container.BindInRequestScope<IUserService, UserService>();
            container.BindInRequestScope<IVehicleService, VehicleService>();
            container.BindInRequestScope<IMovementService, MovementService>();

        }
    }
}