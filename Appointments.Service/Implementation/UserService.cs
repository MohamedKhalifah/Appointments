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
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void ValidateLogin(LoginViewModel userVM)
        {
            string encodedPwd = Encode(userVM.Password);
            var users = _unitOfWork.UserRepository.Get(u =>
                u.UserName == userVM.UserName
                && u.Password == encodedPwd);
            if (users.Count() == 0)
            {
                throw new Exception("Invalid Username or password");                
            }
        }

        public void Register(UserViewModel userVM)
        {
            ValidateUser(userVM);
            var user = Mapper.Map<User>(userVM);
            user.Password = Encode(user.Password);
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();
        }

        void ValidateUser(UserViewModel userVM)
        {
            var users = _unitOfWork.UserRepository.Get(u => u.UserName == userVM.UserName);
            if (users.Count() > 0)
            {
                throw new Exception("Username exist. Enter a different username");
            }
        }

        string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}
