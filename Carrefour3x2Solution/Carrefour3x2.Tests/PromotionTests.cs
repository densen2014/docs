
using Carrefour3x2.Core;
using Xunit;
using System.Collections.Generic;

public class PromotionTests
{
    [Fact]
    public void ThreeItems_CheapestFree()
    {
        var apple = new Product("A", "Apple", 3m, "G1");
        var orange = new Product("O", "Orange", 2m, "G1");

        var cart = new List<CartItem>
        {
            new CartItem(apple, 1),
            new CartItem(orange, 2)
        };

        var rules = new List<PromotionRule>
        {
            new PromotionRule("G1", 3, 2)
        };

        var result = PromotionCalculator.Calculate(cart, rules);

        Assert.Equal(2m, result.Discount);
        Assert.Equal(5m, result.PayTotal);
    }
}
