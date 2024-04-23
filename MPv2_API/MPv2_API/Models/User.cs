using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MPv2_API.Models;

public partial class User {
    [JsonIgnore]
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

    [JsonIgnore]
    public virtual ICollection<Practitioner> Practitioners { get; set; } = new List<Practitioner>();

    [JsonIgnore]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
