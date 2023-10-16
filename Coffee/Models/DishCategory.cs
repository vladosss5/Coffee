using System;
using System.Collections.Generic;

namespace Coffee.Models;

public partial class DishCategory
{
    public int IdList { get; set; }

    public int IdCategory { get; set; }

    public int IdDish { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual Dish IdDishNavigation { get; set; } = null!;
}
