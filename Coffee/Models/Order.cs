﻿using System;
using System.Collections.Generic;

namespace Coffee.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public float FullPrice { get; set; }

    public DateTime DateAndTime { get; set; }

    public int IdStatus { get; set; }

    public virtual ICollection<Cooking> Cookings { get; set; } = new List<Cooking>();

    public virtual StatusesOrder IdStatusNavigation { get; set; } = null!;

    public virtual ICollection<OrderDish> OrderDishes { get; set; } = new List<OrderDish>();
}
