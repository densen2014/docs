using Carrefour3x2.ConsoleApp;
using Carrefour3x2.Core;
using System;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("基础 3x2");
var apple = new Product("A001", "Manzana", 3.00m, "3X2_FRUTA");
var orange = new Product("O001", "Naranja", 2.00m, "3X2_FRUTA");
var milk = new Product("M001", "Leche", 5.0m, null);
var beer = new Product("B001", "Cerveza", 1.50m, "BEBIDAS");
var banana = new Product("B001", "Plátano", 1.50m, "3X2_FRUTA");
var leche = new Product("L001", "Leche Entera", 0.95m, "3X2_LACTEOS");
var agua = new Product("W001", "Agua Mineral", 0.60m, null);

var cart = new List<CartItem>
{
    new CartItem(apple, 1),
    new CartItem(orange, 2),
    new CartItem(milk, 1),
    new CartItem(beer, 6)
};

var rules = new List<PromotionRule>
{
    // 高优先级：3x2
    new PromotionRule(
        groupId: "BEBIDAS",
        buyQuantity: 3,
        payQuantity: 2,
        priority: 100,
        isExclusive: true
    ),

    // 低优先级：买2打9折（示例）
    new PromotionRule(
        groupId: "BEBIDAS",
        buyQuantity: 2,
        payQuantity: 2,
        priority: 10,
        isExclusive: true
    ),

    new PromotionRule(
        groupId: "3X2_FRUTA",
        buyQuantity: 3,
        payQuantity: 2,
        priority: 100,
        isExclusive: true
    ),

    new PromotionRule(
        groupId: "3X2_LACTEOS",
        buyQuantity: 3,
        payQuantity: 2,
        priority: 100,
        isExclusive: true
    ),
};

var result = PromotionCalculator.Calculate(cart, rules);
ReceiptPrinter.Print(result);

Console.WriteLine("\n\n\n模拟数据方案一：基础 3x2（同组不同价）");


cart = new List<CartItem>
{
    new CartItem(apple, 1),
    new CartItem(orange, 2)
};
result = PromotionCalculator.Calculate(cart, rules);
ReceiptPrinter.Print(result);


Console.WriteLine("\n\n\n模拟数据方案二：6 件 → 免 2 件（真实高频）");
var yogurt = new Product("Y001", "Yogur Natural", 1.20m, "3X2_LACTEOS");

cart = new List<CartItem>
{
    new CartItem(yogurt, 6)
};
result = PromotionCalculator.Calculate(cart, rules);
ReceiptPrinter.Print(result);


Console.WriteLine("\n\n\n模拟数据方案三：多促销组 + 非促销商品");

cart = new List<CartItem>
{
    new CartItem(banana, 3),
    new CartItem(leche, 3),
    new CartItem(agua, 2)
};
result = PromotionCalculator.Calculate(cart, rules);
ReceiptPrinter.Print(result);

