// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.ComponentModel;

namespace DensenMP;

public class Products : Product
{
    public string? Message { get; set; }
    public string? Total { get; set; }
    public string? Command { get; set; }
}

public class Product
{
    [DisplayName("序号")]
    public int ID { get; set; }

    [DisplayName("条码")]
    public string? BarCode { get; set; }

    [DisplayName("商品名称")]
    public string? Name { get; set; }

    [DisplayName("备注")]
    public string? Remark { get; set; }

    [DisplayName("价格")]
    public string? Price { get; set; }

    [DisplayName("Base64编码图片")]
    public string? Image { get; set; }

    [DisplayName("商品图片 Url")]
    public string? Url { get; set; }

    [DisplayName("数量")]
    public decimal? Quantity { get; set; }

    public Product(string productName, string price)
    {
        Name = productName;
        Price = price;
    }


    public Product(string productName, string remark, string price, string image, string url)
    {
        Name = productName;
        Remark = remark;
        Price = price;
        Image = image;
        Url = url;
    }

    public Product() { }

}
