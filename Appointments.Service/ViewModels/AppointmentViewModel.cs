using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appointments.Service.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public long PatientId { get; set; }
        [Required]
        [Display(Name="Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Patient Code")]
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public char Gender { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public string BirthDate { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }
        public string Color { get; set; }
    }
}