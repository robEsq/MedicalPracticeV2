using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class Practitioner
{
    public int PractitionerId { get; set; }

    public int UserId { get; set; }

    public string MedicalRegistrationNo { get; set; } = null!;

    public int PracTypeId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual PractitionerType PracType { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Day> Days { get; set; } = new List<Day>();
}
