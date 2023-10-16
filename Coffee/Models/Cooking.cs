using System;
using System.Collections.Generic;

namespace Coffee.Models;

public partial class Cooking
{
    public int IdCooking { get; set; }

    public int IdUser { get; set; }

    public int IdOrder { get; set; }

    public DateTime DateAdmission { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
