// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace DensenMP;

public class ResponseToken
{
    /// <summary>
    /// 附带信息,错误信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 查询令牌 
    /// </summary>
    public int? Token { get; set; }

    /// <summary>
    /// 数据过期时间
    /// </summary>
    public DateTime? Expiration { get; set; }

}

public class ResponseProduct : ResponseToken
{
    /// <summary>
    /// 商品列表
    /// </summary>
    public List<ProductsApi>? Items { get; set; }
}
