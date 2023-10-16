using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee.Models;

public partial class Promotion
{
    public int IdPromotion { get; set; }

    public int IdDish { get; set; }

    public DateTime FinishDate { get; set; }

    public int DiscountPercent { get; set; }

    public double TotalPrice { get; set; }
    
    [NotMapped] public string Activity { get; set; }

    public virtual Dish IdDishNavigation { get; set; } = null!;
}
