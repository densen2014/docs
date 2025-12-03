// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DensenMP;

public partial class ProductUpdateDto
{

    [DisplayName("ID")]
    public int ProductID { get; set; }

    [DisplayName("编号")]
    public string? UserCode { get; set; }

    [DisplayName("条码")]
    public string? BarCode { get; set; }

    [DisplayName("名称")]
    public string? ProductName { get; set; }

    [DisplayName("进价")]
    public decimal? PricePurchase { get; set; } = 0;

    [DisplayName("售价")]
    public decimal UnitPrice { get; set; }

    [DisplayName("售价2")]
    public decimal? UnitPrice2 { get; set; }

    [DisplayName("售价3")]
    public decimal? UnitPrice3 { get; set; }

    [DisplayName("售价4")]
    public decimal? UnitPrice4 { get; set; }

    [DisplayName("售价5")]
    public decimal? UnitPrice5 { get; set; }

    [DisplayName("售价6")]
    public decimal? UnitPrice6 { get; set; }

    [DisplayName("售价7")]
    public decimal? UnitPrice7 { get; set; }

    [DisplayName("售价8")]
    public decimal? UnitPrice8 { get; set; }

    [DisplayName("税%")]
    public float? Tax { get; set; }

    [DisplayName("折扣")]
    public int Discount { get; set; }

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

}
