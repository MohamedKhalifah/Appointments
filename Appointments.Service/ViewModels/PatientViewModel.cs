using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointments.Service.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Birthdate { get; set; }
        public int CountryId { get; set; }
        public char Gender { get; set; }
        public string Phone { get; set; }
    }
}