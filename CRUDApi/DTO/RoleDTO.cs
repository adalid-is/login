using CRUDApi.DTO;

namespace CRUDApi.Data
{
    public class RoleDto

    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
    public class CreateRoleDTO
    {
        public string Description { get; set; } = string.Empty;
    }
    public class EditRoleDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}