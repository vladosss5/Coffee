using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee.Models;

public partial class Post
{
    public int IdPost { get; set; }

    public string Name { get; set; } = null!;
    
    [NotMapped] public bool SelectPost { get; set; } = false;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
