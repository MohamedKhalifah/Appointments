using Appointments.Data;
using Appointments.Service.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Service
{
    public class ServiceBootstrapper
    {
        public static void Bootstrap()
        {
            CreateMaps();
        }
        static void CreateMaps()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Appointment, AppointmentViewModel>()
                    .ForMember(vm => vm.FullName, m => m.MapFrom(a => a.Patient.FullName))
                    .ForMember(vm => vm.Code, m => m.MapFrom(a => a.Patient.Code))
                    .ForMember(vm => vm.CountryId, m => m.MapFrom(a => a.Patient.CountryId))
                    .ForMember(vm => vm.Gender, m => m.MapFrom(a => a.Patient.Gender))
                    .ForMember(vm => vm.Address, m => m.MapFrom(a => a.Patient.Address))
                    .ForMember(vm => vm.Phone, m => m.MapFrom(a => a.Patient.Phone))
                    .ForMember(vm => vm.BirthDate, m => m.MapFrom(a => a.Patient.BirthDate.HasValue ? a.Patient.BirthDate.Value.ToString("dd-MM-yyyy") : string.Empty))
                    .ReverseMap();

                cfg.CreateMap<Patient, AppointmentViewModel>()
                    .ForMember(vm => vm.Code, m => m.MapFrom(a => a.Code))
                    .ForMember(vm => vm.PatientId, m => m.MapFrom(a => a.ID))
                    .ReverseMap();

                cfg.CreateMap<Patient, PatientViewModel>().ReverseMap();

                cfg.CreateMap<Country, CountryViewModel>().ReverseMap();

                cfg.CreateMap<User, UserViewModel>().ReverseMap();

                cfg.CreateMap<AppointmentType, AppointmentTypeViewModel>().ReverseMap();

                cfg.CreateMap<AppointmentStatus, AppointmentStatusViewModel>().ReverseMap();

                cfg.CreateMap<Appointment, EventModel>()
                    .ForMember(vm => vm.Color, m => m.MapFrom(a => a.AppointmentType.Color))
                    .ForMember(vm => vm.PatientName, m => m.MapFrom(a => a.Patient.FullName))
                    .ForMember(vm => vm.Status, m => m.MapFrom(a => a.AppointmentStatus.Name))
                    .ForMember(vm => vm.TypeId, m => m.MapFrom(a => a.TypeId))
                    .ReverseMap();
            });
        }
    }
}
