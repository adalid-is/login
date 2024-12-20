using System;
using System.Collections.Generic;

namespace CRUDApi.Models;

public partial class City
{
    public int Id { get; set; }
    public string Description { get; set; }

    public int IdProvince { get; set; }  // Clave foránea a Province
    public virtual Province Province { get; set; }  // Propiedad de navegación a Province
    public virtual ICollection<People> Peoples { get; set; } = new List<People>();
}
