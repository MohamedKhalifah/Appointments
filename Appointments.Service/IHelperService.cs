using Appointments.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Service
{
    public interface IHelperService
    {
        List<CountryViewModel> GetCountries();
        List<AppointmentTypeViewModel> GetTypes();
        List<AppointmentStatusViewModel> GetStatuses();
    }
}
