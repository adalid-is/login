using CRUDApi.DTO;

namespace CRUDApi.Data
{
    public class PeopleDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int IdCity { get; set; }
        public CityDTO City { get; set; }

    }

    public class CreatePeopleDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int IdCity { get; set; }
    }

    public class EditPeopleDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int IdCity { get; set; }
    }
}
