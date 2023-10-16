using System;
using System.Collections.Generic;

namespace Coffee.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public string? Photo { get; set; }

    public virtual ICollection<DishCategory> DishCategories { get; set; } = new List<DishCategory>();
}
