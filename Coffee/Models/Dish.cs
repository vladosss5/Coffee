using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee.Models;

public partial class Dish
{
    public int IdDish { get; set; }

    public string Name { get; set; } = null!;

    public float Price { get; set; }

    public string Photo { get; set; } = null!;

   
    [NotMapped] public int Count { get; set; } = 1;
    public virtual ICollection<DishCategory> DishCategories { get; set; } = new List<DishCategory>();

    public virtual ICollection<OrderDish> OrderDishes { get; set; } = new List<OrderDish>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
