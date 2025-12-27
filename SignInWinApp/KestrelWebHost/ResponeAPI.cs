// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

namespace JovenApi;

public class ResponeAPI
{
    public string? Message { get; set; }
    public int? Token { get; set; }
    public DateTime? Expiration { get; set; }
    public object? Items { get; set; }

    public ResponeAPI() { }

    public ResponeAPI(string? message)
    {
        Message = message;
    }

    public ResponeAPI(string? message, int? token)
    {
        Message = message;
        Token = token;
    }

    public ResponeAPI(string? message, DateTime expiration, object? items)
    {
        Message = message;
        Expiration = expiration;
        Items = items;
    }

    public ResponeAPI(DateTime expiration, object? items)
    {
        Expiration = expiration;
        Items = items;
    }
}
