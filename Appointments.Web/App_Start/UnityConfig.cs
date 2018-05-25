using Appointments.Data.Repositories;
using Appointments.Data.Repositories.Interfaces;
using Appointments.Service;
using Appointments.Service.Implementation;
using Appointments.Service.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Appointments.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAppointmentService, AppointmentService>();
            container.RegisterType<IHelperService, HelperService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IPatientService, PatientService>();
            container.RegisterType<IUserService, UserService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}