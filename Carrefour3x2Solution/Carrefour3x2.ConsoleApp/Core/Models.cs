
namespace Carrefour3x2.Core;

public record Product(string Sku, string Name, decimal UnitPrice, string? PromotionGroupId);

public record CartItem(Product Product, int Quantity);

public class PromotionRule
{
    public string GroupId { get; }
    public int BuyQuantity { get; }
    public int PayQuantity { get; }

    /// <summary>
    /// 优先级（越大越先算）
    /// </summary>
    public int Priority { get; }

    /// <summary>
    /// 是否互斥
    /// </summary>
    public bool IsExclusive { get; }  

    public PromotionRule(
        string groupId,
        int buyQuantity,
        int payQuantity,
        int priority,
        bool isExclusive = true)
    {
        GroupId = groupId;
        BuyQuantity = buyQuantity;
        PayQuantity = payQuantity;
        Priority = priority;
        IsExclusive = isExclusive;
    }
}

public class SettlementResult
{
    public decimal OriginalTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal PayTotal { get; set; }

    public List<ReceiptLine> Lines { get; set; } = new();
}

public class ReceiptLine
{
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
}

/// <summary>
/// 内部使用：单件商品模型（不暴露给外部）
/// </summary>
internal class UnitItem
{
    public Product Product { get; }
    public decimal Price => Product.UnitPrice;

    public bool Used { get; set; } = false;

    public UnitItem(Product product)
    {
        Product = product;
    }
}
