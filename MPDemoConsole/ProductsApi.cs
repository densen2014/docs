// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DensenMP;

public partial class ProductsApi
{

    [DisplayName("ID")]
    public int ProductID { get; set; }

    [DisplayName("编号")]
    public string? UserCode { get; set; }

    [DisplayName("条码")]
    public string? BarCode { get; set; }

    [DisplayName("名称")]
    public string? ProductName { get; set; }

    [DisplayName("库存")]
    public double? UnitsInStock { get; set; } = 0;

    [DisplayName("进价")]
    public decimal? PricePurchase { get; set; } = 0;

    [DisplayName("售价")]
    public decimal UnitPrice { get; set; }

    [DisplayName("税%")]
    public float? Tax { get; set; }

    [DisplayName("折扣")]
    public int Discount { get; set; }

    [DisplayName("数量")]
    public int Quantity { get => quantity ?? Math.Max(1, QuantityPerUnit); set => quantity = value; }
    private int? quantity;

    [DisplayName("名称")]
    public string? ProductNameIn { get; set; }

    [DisplayName("售价")]
    public int? UnitPriceIn { get; set; }

    [DisplayName("折扣")]
    public int? DiscountIn { get; set; }

    [DisplayName("数量")]
    public int? Inventory { get; set; }

    /// <summary>
    /// 小包装
    /// </summary>
    [DisplayName("小包")]
    [JsonPropertyOrder(100)]
    public int QuantityPerUnit { get; set; } = 1;

    /// <summary>
    /// 大包装
    /// </summary>
    [DisplayName("大包")]
    [JsonPropertyOrder(101)]
    public int QuantityPerUnit2nd { get; set; } = 1;

    /// <summary>
    /// 进货量
    /// </summary>
    [DisplayName("进货量")]
    public double? UnitsOnPurchase { get; set; } = 0;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [DisplayName("状态")]
    [JsonPropertyOrder(102)]
    public ProductStatus MarketingNO { get; set; } = ProductStatus.Normal;

    /// <summary>
    /// 编辑日期
    /// </summary>
    [DisplayName("编辑日期")]
    [JsonPropertyOrder(6)]
    public string? LastModify { get; set; }


    [JsonPropertyOrder(7)]
    [DisplayName("存放位置")]
    public string? StorageLocation { get; set; }
}
public enum ProductStatus
{
    Normal = 0,
    Reserve = 1,
    Discontinued = 2,
    OutOfStock = 3,
    Generally = 4,
    SpecialOffer = 5,
    Promotion = 6,
    HotSell = 7,
    NewProducts = 8,
    NotSet = -1,
}