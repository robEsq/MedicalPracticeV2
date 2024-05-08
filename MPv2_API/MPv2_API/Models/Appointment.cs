using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class Appointment
{
    public int AppId { get; set; }

    public DateOnly AppDate { get; set; }

    public TimeOnly AppTime { get; set; }

    public int PractitionerId { get; set; }

    public int PatientId { get; set; }

    public virtual User Patient { get; set; } = null!;

    public virtual Practitioner Practitioner { get; set; } = null!;
}
