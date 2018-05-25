using Appointments.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AppointmentsEntities context = new AppointmentsEntities();
        private GenericRepository<Appointment> appointmentRepository;
        private GenericRepository<Patient> patientRepository;
        private GenericRepository<Country> countryRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<AppointmentType> appointmentTypeRepository;
        private GenericRepository<AppointmentStatus> appointmentStatusRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }        

        public GenericRepository<Appointment> AppointmentRepository
        {
            get
            {
                if (this.appointmentRepository == null)
                {
                    this.appointmentRepository = new GenericRepository<Appointment>(context);
                }
                return appointmentRepository;
            }
        }

        public GenericRepository<Patient> PatientRepository
        {
            get
            {
                if (this.patientRepository == null)
                {
                    this.patientRepository = new GenericRepository<Patient>(context);
                }
                return patientRepository;
            }
        }

        public GenericRepository<Country> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                {
                    this.countryRepository = new GenericRepository<Country>(context);
                }
                return countryRepository;
            }
        }

        public GenericRepository<AppointmentType> AppointmentTypeRepository
        {
            get
            {
                if (this.appointmentTypeRepository == null)
                {
                    this.appointmentTypeRepository = new GenericRepository<AppointmentType>(context);
                }
                return appointmentTypeRepository;
            }
        }

        public GenericRepository<AppointmentStatus> AppointmentStatusRepository
        {
            get
            {
                if (this.appointmentStatusRepository == null)
                {
                    this.appointmentStatusRepository = new GenericRepository<AppointmentStatus>(context);
                }
                return appointmentStatusRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
