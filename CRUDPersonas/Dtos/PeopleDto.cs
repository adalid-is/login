using System.Text.Json.Serialization;

namespace CRUDPersonas.Dto;

public class PeopleDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Nationality { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public int IdCity { get; set; }
    public CityDto City { get; set; }
    public string CityDescription => City?.Description ?? "Sin ciudad";
    public string ProvinceDescription => City?.Province?.Description ?? "Sin provincia";

}