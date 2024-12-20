using System;
using System.Collections.Generic;

namespace CRUDApi.Models;

public partial class People
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Nationality { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

    public int IdCity { get; set; } // Clave foránea a City
    public virtual City City { get; set; }   // Propiedad de navegación a City
}
