using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Title { get; set; }

    public string FName { get; set; } = null!;

    public string? MiddleInitial { get; set; }

    public string LName { get; set; } = null!;

    public string? MedicareNo { get; set; }

    public string? HomePhoneNo { get; set; }

    public string? MobilePhoneNo { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Practitioner> Practitioners { get; set; } = new List<Practitioner>();
}
