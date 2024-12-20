namespace CRUDApi.DTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int IdProvince { get; set; }
        public RoleDTO Province { get; set; }
    }

    public class CreateCityDTO
    {
        public string Description { get; set; }
        public int IdProvince { get; set; }
    }

    public class EditCityDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int IdProvince { get; set; }
    }
}
