using System;
using System.Collections.Generic;

namespace Coffee.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fname { get; set; } = null!;

    public string Sname { get; set; } = null!;

    public string? Lname { get; set; }

    public int IdPost { get; set; }

    public virtual ICollection<Cooking> Cookings { get; set; } = new List<Cooking>();

    public virtual Post IdPostNavigation { get; set; } = null!;
}
