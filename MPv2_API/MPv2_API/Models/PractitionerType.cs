using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class PractitionerType
{
    public int PracTypeId { get; set; }

    public string PracTypeName { get; set; } = null!;

    public virtual ICollection<Practitioner> Practitioners { get; set; } = new List<Practitioner>();
}
