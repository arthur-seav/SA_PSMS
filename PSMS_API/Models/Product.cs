using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Product
{
    public int ProdId { get; set; }

    public string ProdName { get; set; } = null!;

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public int MinStockLevel { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Condition { get; set; } = null!;

    public int BrandId { get; set; }

    public int? SeriesId { get; set; }

    public int CategoryId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual Series? Series { get; set; }
}
