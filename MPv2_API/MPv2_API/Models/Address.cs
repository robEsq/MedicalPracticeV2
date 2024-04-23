using System;
using System.Collections.Generic;

namespace MPv2_API.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string HouseUnitLotNo { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Suburb { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Postcode { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
