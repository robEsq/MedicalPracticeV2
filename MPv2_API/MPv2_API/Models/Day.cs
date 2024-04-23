using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class Day
{
    public int DayId { get; set; }

    public string DayName { get; set; } = null!;

    public virtual ICollection<Practitioner> Practitioners { get; set; } = new List<Practitioner>();
}
