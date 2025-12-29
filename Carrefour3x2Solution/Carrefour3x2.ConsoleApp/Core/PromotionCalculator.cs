namespace Carrefour3x2.Core;


/// <summary>
/// 促销计算引擎
/// </summary>
public static class PromotionCalculator
{
    public static SettlementResult Calculate(
        List<CartItem> cart,
        List<PromotionRule> rules)
    {
        var result = new SettlementResult();

        // 1️ 商品行 & 原价
        var units = new List<UnitItem>();

        foreach (var item in cart)
        {
            var amount = item.Product.UnitPrice * item.Quantity;
            result.OriginalTotal += amount;

            result.Lines.Add(new ReceiptLine
            {
                Description = $"{item.Product.Name} x{item.Quantity}",
                Amount = amount
            });

            for (int i = 0; i < item.Quantity; i++)
                units.Add(new UnitItem(item.Product));
        }

        // 2️ 按优先级排序促销（高 → 低）
        foreach (var rule in rules.OrderByDescending(r => r.Priority))
        {
            // 可参与此促销的“未被占用”的商品
            var candidates = units
                .Where(u =>
                    !u.Used &&
                    u.Product.PromotionGroupId == rule.GroupId)
                .ToList();

            int bundleSize = rule.BuyQuantity;
            int freeCountPerBundle = rule.BuyQuantity - rule.PayQuantity;

            int freeCount = (candidates.Count / bundleSize) * freeCountPerBundle;
            if (freeCount <= 0)
                continue;

            // 3 最便宜的免费
            var freeItems = candidates
                .OrderBy(u => u.Price)
                .Take(freeCount)
                .ToList();

            decimal discount = freeItems.Sum(i => i.Price);
            result.Discount += discount;

            result.Lines.Add(new ReceiptLine
            {
                Description = $"PROMOCIÓN {rule.GroupId}",
                Amount = -discount
            });

            // 4️ 互斥处理：标记商品已使用
            if (rule.IsExclusive)
            {
                foreach (var item in freeItems)
                    item.Used = true;
            }
        }

        result.PayTotal = result.OriginalTotal - result.Discount;
        return result;
    }
}
