using Appointments.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Service.Interfaces
{
    public interface IPatientService
    {
        bool IsUniqueCode(AppointmentViewModel model);
        List<PatientViewModel> Search(PatientViewModel patientVM);
        PatientViewModel GetPatient(long id);
    }
}
