// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Reflection;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true) // 可选：如果没有用户机密，不会抛出异常
    .Build();

//1. 联系客服获取
var densenID = "123456789XCXUQ14mg==";

var endpoint = "https://mp.app1.es";

// 也可以从用户机密读取 densenID
densenID = configuration["DensenID"] ?? "123456789XCXUQ14mg==";
endpoint = configuration["DensenEndpoint"] ?? "https://mp.app1.es";

var barcode = "8437008795515";

var client = new HttpClient();

//2. 获取 api token
var densenResult = await client.GetFromJsonAsync<DensenMP.ResponseToken>($"{endpoint}/api/getProduct/{densenID}");

if (densenResult == null || string.IsNullOrEmpty(densenResult.Token.ToString()))
{
    return;
}

//3. 获取商品列表

//使用token获取商品资料, 缓存5分钟. 意思是5分钟内都是同一份数据.降低客户数据主机压力

//{endpoint}/api/getProduct/{densenID}/{token}

var response = await client.GetFromJsonAsync<DensenMP.ResponseProduct>($"{endpoint}/api/getProduct/{densenID}/{densenResult.Token}");
if (response?.Items != null && response.Items.Count >= 0)
{
    // 处理响应数据
    Console.WriteLine($"Product List:\r\n{"UserCode",-8}\t{"BarCode",-15}\t{"ProductName",-15}\t{"UnitPrice",8}");
    foreach (var item in response.Items.Take(10))
    {
        Console.WriteLine($"{item.UserCode,-8}\t{item.BarCode,-15}\t{item.ProductName,-15}\t{item.UnitPrice:n2}");
    }
}
else
{
    Console.WriteLine("No items found or invalid response.");
}
await Task.Delay(1000);


//4.添加商品 Post

//{endpoint}/api/addProduct/{densenID}/{int:token}
//FromBody ProductUpdateDto

var newProduct = new DensenMP.ProductUpdateDto
{
    UserCode = "TEST001",
    BarCode = barcode,
    ProductName = "测试商品",
    UnitPrice = 9.99M,
    UnitPrice2 = 8.99M,
};
var postResponse = await client.PostAsJsonAsync($"{endpoint}/api/addProduct/{densenID}/{densenResult.Token}", newProduct);
var postResult = await postResponse.Content.ReadAsStringAsync();
Console.WriteLine($"\r\n\r\nAdd Product Response: {postResult}");
await Task.Delay(1000);


//5.编辑商品 Post

//{endpoint}/api/updateProduct/{densenID}/{int:token}
//FromBody ProductUpdateDto

var updateProduct = new DensenMP.ProductUpdateDto
{
    UserCode = "TEST001",
    BarCode = barcode,
    ProductName = "测试商品-更新",
    UnitPrice = 10.99M,
    UnitPrice2 = 9.99M,
    UnitPrice3 = 8.99M,
    UnitPrice8 = 5.99M,
};

var updateResponse = await client.PostAsJsonAsync($"{endpoint}/api/updateProduct/{densenID}/{densenResult.Token}", updateProduct);
var updateResult = await updateResponse.Content.ReadAsStringAsync();
Console.WriteLine($"\r\n\r\nUpdate Product Response: {updateResult}");
await Task.Delay(1000);

//6. 查询商品

//{endpoint}/api/getProduct/{densenID}/{token}/{barcode}

response = await client.GetFromJsonAsync<DensenMP.ResponseProduct>($"{endpoint}/api/getProduct/{densenID}/{densenResult.Token}/{barcode}");
if (response?.Items != null && response.Items.Count >= 0)
{
    // 处理响应数据
    Console.WriteLine($"\r\n\r\nProduct:");
    foreach (var item in response.Items)
    {
        Console.WriteLine($"Product UserCode: {item.UserCode}");
        Console.WriteLine($"Product BarCode: {item.BarCode}");
        Console.WriteLine($"Product Name: {item.ProductName}");
        Console.WriteLine($"Product Price: {item.UnitPrice}");
        Console.WriteLine($"Product Price2: {item.UnitPrice2}");
        Console.WriteLine($"Product Price3: {item.UnitPrice3}");
        Console.WriteLine($"Product Price4: {item.UnitPrice4}");
        Console.WriteLine($"Product Price5: {item.UnitPrice5}");
        Console.WriteLine($"Product Price6: {item.UnitPrice6}");
        Console.WriteLine($"Product Price7: {item.UnitPrice7}");
        Console.WriteLine($"Product Price8: {item.UnitPrice8}");
    }
}
else
{
    Console.WriteLine("No items found or invalid response.");
}

//7.盘点库存 Get

//{endpoint}/api/changeQuantity/{densenID}/{int:token}/{barcode}/{int:quantity}

