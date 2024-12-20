using CRUDApi.DTO;

namespace CRUDApi.Data
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? PeopleId { get; set; }
        public PeopleDTO? People { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();

    }

    public class CreateUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? PeopleId { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

    public class EditUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public int? PeopleId { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
