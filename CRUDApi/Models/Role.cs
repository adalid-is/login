using System;
using System.Collections.Generic;

namespace CRUDApi.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    //public virtual ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public static implicit operator Role(List<Role> v)
    {
        throw new NotImplementedException();
    }
}
