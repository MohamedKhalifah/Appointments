using Appointments.Data.Repositories.Interfaces;
using Appointments.Service.Interfaces;
using Appointments.Service.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private IUnitOfWork _unitOfWork;
        public PatientService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool IsUniqueCode(AppointmentViewModel model)
        {
            var patient = _unitOfWork.PatientRepository.Get(p => p.Code == model.Code && (model.PatientId == 0 || model.PatientId != p.ID));
            return (patient == null || patient.Count() == 0);
        }
        public List<PatientViewModel> Search(PatientViewModel patientVM)
        {
            var patients = _unitOfWork.PatientRepository.Get(p => 
                ((!string.IsNullOrEmpty(patientVM.FullName) ? p.FullName.Contains(patientVM.FullName) : false)
                || (!string.IsNullOrEmpty(patientVM.Code) ? p.Code == patientVM.Code : false)
                || (p.BirthDate.HasValue ? (p.BirthDate.Value.ToString() == patientVM.Birthdate) : false)));
            var patientsVM = Mapper.Map<List<PatientViewModel>>(patients);
            return patientsVM;
        }
        
        public PatientViewModel GetPatient(long id)
        {
            var patient = _unitOfWork.PatientRepository.GetByID(id);
            var patientVM = Mapper.Map<PatientViewModel>(patient);
            return patientVM;
        }
    }
}
