using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appointments.Web.Common
{
    public enum AppointmentStatuses
    {
        Scheduled = 1,
        Arrived,
        Cancelled,
        Completed
    }

    public enum Genders
    {
        Male = 'm',
        Female = 'f',
        Unknown = 'u'
    }
}