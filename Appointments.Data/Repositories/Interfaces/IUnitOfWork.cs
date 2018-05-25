using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Appointment> AppointmentRepository { get; }
        GenericRepository<Patient> PatientRepository { get; }
        GenericRepository<Country> CountryRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<AppointmentType> AppointmentTypeRepository { get; }
        GenericRepository<AppointmentStatus> AppointmentStatusRepository { get; }
        void Save();
    }
}
