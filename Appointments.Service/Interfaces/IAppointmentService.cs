using Appointments.Data;
using Appointments.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Service.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<EventModel> GetEvents(DateTime start, DateTime end);
        AppointmentViewModel GetAppointment(long id);
        void SaveAppointment(AppointmentViewModel appointment);
        void DeleteAppointment(long id);
        void ChangeAppointmentStatus(long id, int statusId);
    }
}
