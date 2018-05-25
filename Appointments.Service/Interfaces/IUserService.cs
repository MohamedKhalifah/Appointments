using Appointments.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Service.Interfaces
{
    public interface IUserService
    {
        void ValidateLogin(LoginViewModel user);
        void Register(UserViewModel user);        
    }
}
