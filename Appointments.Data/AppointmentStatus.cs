//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Appointments.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppointmentStatus
    {
        public AppointmentStatus()
        {
            this.Appointments = new HashSet<Appointment>();
        }
    
        public byte ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
