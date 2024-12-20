using System;
using System.Collections.Generic;

namespace CRUDApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public int PeopleId { get; set; }

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public virtual People People { get; set; } = null!;
}
