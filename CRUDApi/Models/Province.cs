using System;
using System.Collections.Generic;

namespace CRUDApi.Models;

public partial class Province
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
