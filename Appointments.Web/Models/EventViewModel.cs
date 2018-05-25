using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointments.Web.Models
{
    public class EventViewModel : ISchedulerEvent
    {
        public string PatientName { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public long AppointmentId { get; set; }
        public string Description { get; set; }
        public DateTime End { get; set; }
        public string EndTimezone { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceException { get; set; }
        public string RecurrenceRule { get; set; }
        public DateTime Start { get; set; }        
        public string StartTimezone { get; set; }        
        public string Title { get; set; }
        public int TypeId { get; set; }
    }
}