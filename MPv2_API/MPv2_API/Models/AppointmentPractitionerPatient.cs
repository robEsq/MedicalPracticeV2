using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class AppointmentPractitionerPatient
{
    public int AppId { get; set; }

    public int PractitionerId { get; set; }

    public int PatientId { get; set; }

    public virtual Appointment App { get; set; } = null!;

    public virtual User Patient { get; set; } = null!;

    public virtual Practitioner Practitioner { get; set; } = null!;
}
