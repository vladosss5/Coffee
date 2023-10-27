using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public string? Photo { get; set; }
    [NotMapped] public bool SelectCategory { get; set; } = false; 

    public virtual ICollection<DishCategory> DishCategories { get; set; } = new List<DishCategory>();
}
