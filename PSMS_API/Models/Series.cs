using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Series
{
    public int SeriesId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
