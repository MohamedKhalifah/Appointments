using Appointments.Data;
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
    public class AppointmentService : IAppointmentService, IDisposable
    {
        private IUnitOfWork _unitOfWork;
        private IPatientService _patientService;
        public AppointmentService(IUnitOfWork unitOfWork, IPatientService patientService)
        {
            this._unitOfWork = unitOfWork;
            this._patientService = patientService;
        }

        void ValidateAppointment(AppointmentViewModel model)
        {
            var isUniqueCode = _patientService.IsUniqueCode(model);
            StringBuilder messages = null;
            if(!isUniqueCode)
            {
                messages = new StringBuilder();
                messages.Append("Patient code must be unique");
            }
            if (messages != null)
            {
                throw new Exception(messages.ToString());
            }
        }
        
        public AppointmentViewModel GetAppointment(long id)
        {
            var appointment = _unitOfWork.AppointmentRepository.GetByID(id);
            var appointmentVM = Mapper.Map<AppointmentViewModel>(appointment);

            return appointmentVM;
        }

        public void SaveAppointment(AppointmentViewModel appointmentVM)
        {
            ValidateAppointment(appointmentVM);
            var appointment = Mapper.Map<Appointment>(appointmentVM);
            var patient = Mapper.Map<Patient>(appointmentVM);
            if (appointmentVM.Id > 0)
            {
                var patientToUpdate = _unitOfWork.PatientRepository.GetByID(appointmentVM.PatientId);
                patientToUpdate.Address = patient.Address;
                patientToUpdate.BirthDate = patient.BirthDate;
                patientToUpdate.Code = patient.Code;
                patientToUpdate.CountryId = patient.CountryId;
                patientToUpdate.FullName = patient.FullName;
                patientToUpdate.Gender = patient.Gender;
                patientToUpdate.Phone = patient.Phone;

                var appointmentToUpdate = _unitOfWork.AppointmentRepository.GetByID(appointmentVM.Id);
                appointmentToUpdate.End = appointment.End;
                appointmentToUpdate.PatientId = appointment.PatientId;
                appointmentToUpdate.Start = appointment.Start;
                appointmentToUpdate.StatusId = appointment.StatusId;
                appointmentToUpdate.TypeId = appointment.TypeId;

                _unitOfWork.PatientRepository.Update(patientToUpdate);
                _unitOfWork.AppointmentRepository.Update(appointmentToUpdate);
                _unitOfWork.Save();
            }
            else
            {
                if (appointmentVM.PatientId > 0)
                {
                    var patientToUpdate = _unitOfWork.PatientRepository.GetByID(appointmentVM.PatientId);
                    patientToUpdate.Address = patient.Address;
                    patientToUpdate.BirthDate = patient.BirthDate;
                    patientToUpdate.Code = patient.Code;
                    patientToUpdate.CountryId = patient.CountryId;
                    patientToUpdate.FullName = patient.FullName;
                    patientToUpdate.Gender = patient.Gender;
                    patientToUpdate.Phone = patient.Phone;
                    _unitOfWork.PatientRepository.Update(patientToUpdate);
                }
                else
                {
                    _unitOfWork.PatientRepository.Insert(patient);
                    appointment.PatientId = patient.ID;
                }
                _unitOfWork.AppointmentRepository.Insert(appointment);
                _unitOfWork.Save();
            }
        }

        public void ChangeAppointmentStatus(long id, int statusId)
        {
            var appointment = _unitOfWork.AppointmentRepository.GetByID(id);
            appointment.StatusId = (byte)statusId;
            _unitOfWork.AppointmentRepository.Update(appointment);
            _unitOfWork.Save();
        }

        public void DeleteAppointment(long id)
        {
            _unitOfWork.AppointmentRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<EventModel> GetEvents(DateTime start, DateTime end)
        {
            var appointments = _unitOfWork.AppointmentRepository.Get(a => a.Start >= start && a.End <= end);
            var events = Mapper.Map<IEnumerable<Appointment>, IEnumerable<EventModel>>(appointments);
            return events;
        }

        public void Dispose()
        {
            this._unitOfWork.Dispose();
        }
    }
}
