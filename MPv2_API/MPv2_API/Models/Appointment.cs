using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MPv2_API.Models;

public partial class Appointment
{
    public int AppId { get; set; }

    public DateOnly AppDate { get; set; }

    public TimeOnly AppTime { get; set; }

    [JsonIgnore]
    public virtual ICollection<AppointmentPractitionerPatient> AppointmentPractitionerPatients { get; set; } = new List<AppointmentPractitionerPatient>();
}
