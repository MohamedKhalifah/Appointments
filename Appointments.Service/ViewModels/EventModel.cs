using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointments.Service.ViewModels
{
    public class EventModel
    {
        public long ID { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TypeId { get; set; }
    }
}