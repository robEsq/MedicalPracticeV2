using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class Practitioner
{
    public int PractitionerId { get; set; }

    public int UserId { get; set; }

    public string MedicalRegistrationNo { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Day> Days { get; set; } = new List<Day>();

    public virtual ICollection<PractitionerType> PracTypes { get; set; } = new List<PractitionerType>();
}
