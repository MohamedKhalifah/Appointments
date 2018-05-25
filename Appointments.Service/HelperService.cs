using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointments.Data;
using Appointments.Data.Repositories.Interfaces;
using AutoMapper;
using Appointments.Service.ViewModels;

namespace Appointments.Service
{
    public class HelperService : IHelperService
    {
        private IUnitOfWork _unitOfWork;
        public HelperService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<CountryViewModel> GetCountries()
        {
            var countries = _unitOfWork.CountryRepository.Get();
            var countriesVM = Mapper.Map<IEnumerable<Country>, List<CountryViewModel>>(countries);
            return countriesVM;
        }
        public List<AppointmentTypeViewModel> GetTypes()
        {
            var types = _unitOfWork.AppointmentTypeRepository.Get();
            var typesVM = Mapper.Map<IEnumerable<AppointmentType>, List<AppointmentTypeViewModel>>(types);
            return typesVM;
        }
        public List<AppointmentStatusViewModel> GetStatuses()
        {
            var statuses = _unitOfWork.AppointmentStatusRepository.Get();
            var statusesVM = Mapper.Map<IEnumerable<AppointmentStatus>, List<AppointmentStatusViewModel>>(statuses);
            return statusesVM;
        }
    }
}
