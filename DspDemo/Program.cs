using DensenMP;
using System.Net.Http.Json;
using System.Text.Json;

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 禁用 Unicode 转义
};

var dspIP = "http://192.168.1.146:5000";
var client = new HttpClient() { BaseAddress = new Uri(dspIP) };

var productSample = new Product()
{
    Name = "阿香婆 人头牛马炸酱面",
    Price = "$10.99",
    Url = "https://jovenres.obs.eu-west-101.myhuaweicloud.eu/images/resDemo/avatar/8.jpg?c5e21bb2618dc00ae57408928850f650"
};

decimal total = 0;

var productsSample = new Products()
{
    Name = "阿香婆 人头牛马炸酱面",
    Price = "$10.99",
    Url = "https://jovenres.obs.eu-west-101.myhuaweicloud.eu/images/resDemo/avatar/8.jpg?c5e21bb2618dc00ae57408928850f650",
    Total = "$10.99"
};

while (true)
{
    Console.WriteLine("请选择操作类型：");
    Console.WriteLine("1. put 显示单个商品模式,json Product");
    Console.WriteLine("2. puts 价格显示屏模式,json Products");
    Console.WriteLine("3. get 显示单个商品模式,字符串参数 name,price,image,url,remark");
    Console.WriteLine("4. set 显示单个商品模式,json Product");
    Console.WriteLine("5. saveconfig 设置");
    Console.WriteLine("0. 价格显示屏复位");
    Console.Write("输入数字选择操作: ");

    if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(ApiType), choice))
    {
        decimal Price = DateTime.Now.Minute / 100m;
        var apiType = (ApiType)choice;
        switch (apiType)
        {
            case ApiType.put:
            case ApiType.set:
                productsSample.Price = $"€ {Price:n2}";
                Console.WriteLine(JsonSerializer.Serialize(productSample, options));
                var putResponse = await client.PostAsJsonAsync("put", productSample);
                if (putResponse.IsSuccessStatusCode)
                {
                    var result = await putResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Error: {putResponse.StatusCode}");
                }
                break;

            case ApiType.puts:
                productsSample.Price = $"€ {Price:n2}";
                productsSample.Quantity = DateTime.Now.Second > 1 && DateTime.Now.Second < 6 ? DateTime.Now.Second : null;
                total += Price * productSample.Quantity ?? 1;
                productsSample.Total = $"€ {total:n2}";
                Console.WriteLine(JsonSerializer.Serialize(productsSample, options));
                var putResponseputs = await client.PostAsJsonAsync("puts", productsSample);
                if (putResponseputs.IsSuccessStatusCode)
                {
                    var result = await putResponseputs.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Error: {putResponseputs.StatusCode}");
                }
                break;

            case ApiType.get:
                var getResponse = await client.GetAsync("get");
                if (getResponse.IsSuccessStatusCode)
                {
                    var result = await getResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Error: {getResponse.StatusCode}");
                }
                break;

            case ApiType.saveconfig:
                Console.WriteLine("执行 saveconfig 操作...");
                // 添加对应的逻辑
                break;

            default:
                Console.WriteLine("复位");
                var reset = new Products()
                {
                    Command = "reset"
                };
                var putResponseReset = await client.PostAsJsonAsync("puts", reset);
                if (putResponseReset.IsSuccessStatusCode)
                {
                    var result = await putResponseReset.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Error: {putResponseReset.StatusCode}");
                }
                break;
        }
    }
    else
    {
        Console.WriteLine("无效输入，请输入 1-5 的数字。");
    }

    Console.WriteLine("\r\n\r\n请输入 1-5 的数字，或按 Ctrl+C 退出...\r\n");
}

enum ApiType
{
    reset = 0,
    put = 1,
    puts = 2,
    get = 3,
    set = 4,
    saveconfig = 5
}
