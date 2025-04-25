// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.Net.Http.Json;

//1. 联系客服获取
var densenID = "123456789XCXUQ14mg==";

var barcode = "8437008795515";

var client = new HttpClient();

//2. 获取 api token
var densenResult = await client.GetFromJsonAsync<DensenMP.ResponseToken>($"https://mp.app1.es/api/getProduct/{densenID}");

if (densenResult == null || string.IsNullOrEmpty(densenResult.Token.ToString()))
{
    return;
}

//3. 获取商品列表

//使用token获取商品资料, 缓存5分钟. 意思是5分钟内都是同一份数据.降低客户数据主机压力

//https://mp.app1.es/api/getProduct/{densenID}/{token}


//4. 查询商品

//https://mp.app1.es/api/getProduct/{densenID}/{token}/{barcode}

var response = await client.GetFromJsonAsync<DensenMP.ResponseProduct>($"https://mp.app1.es/api/getProduct/{densenID}/{densenResult.Token}/{barcode}");
if (response?.Items != null && response.Items.Count >= 0)
{
    // 处理响应数据
    foreach (var item in response.Items)
    {
        Console.WriteLine($"Product UserCode: {item.UserCode}");
        Console.WriteLine($"Product BarCode: {item.BarCode}");
        Console.WriteLine($"Product Name: {item.ProductName}");
        Console.WriteLine($"Product Price: {item.UnitPrice}");

    }
}
else
{
    Console.WriteLine("No items found or invalid response.");
}

//5.盘点库存

//https://mp.app1.es/api/changeQuantity/{densenID}/{int:token}/{barcode}/{int:quantity}
