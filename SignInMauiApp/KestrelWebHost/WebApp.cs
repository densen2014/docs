using Microsoft.AspNetCore.Http;
using SignInMauiApp.Models;
using System.Text;
using System.Text.Json;

namespace KestrelWebHost;

public partial class WebApp
{


    private static readonly Dictionary<string, Func<HttpContext, HttpResponse, Task>> RouteHandlers =
        new()
        {
            { "/", async (ctx, res) =>
                {
                    if (ctx.Request.Method == "POST") {
                        await HandleControlRequest(ctx, res);
                    } else {
                        await RenderPlayerControlForm(res);
                    }
                }
            },
        };
    private static async Task RenderPlayerControlForm(HttpResponse response)
    {
        var html = """"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Registro de jornada laboral</title>
    <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css' rel='stylesheet'>
</head>
<body>
    <div class='container mt-5'>
        <h1 class='text-center'>Registro de jornada laboral</h1>
        <form id='productForm' class='mt-4'>
            <div class='mb-3'>
                <label for='username' class='form-label'>Nombre</label>
                <input type='text' class='form-control' id='username' name='username' placeholder='Nombre' value='' required>
            </div>
            <div class='mb-3'>
                <label for='password' class='form-label'>Clave</label>
                <input type='password' class='form-control' id='password' name='password' placeholder='Clave' value='' required>
            </div> 
            <button type='button' class='btn btn-primary mb-3' onclick='submitForm("signin")'>Check-in</button>
            <button type='button' class='btn btn-success mb-3' onclick='submitForm("signout")'>Check-out</button>
            <button type='button' class='btn btn-danger mb-3' onclick='clearCache()'>Borrar Caché</button>
            <h6 id='result' class='text-info'>bienvenido</h6>
        </form>
    </div>
    <script>
        const form = document.getElementById('productForm');
        const result = document.getElementById('result');
        // 在页面加载时恢复输入框的值
        document.addEventListener('DOMContentLoaded', function () {
            const inputs = document.querySelectorAll('input, textarea'); // 选择所有输入框和文本区域
            inputs.forEach(input => {
                const savedValue = localStorage.getItem(input.id); // 从 localStorage 获取值
                if (savedValue) {
                    input.value = savedValue; // 恢复值
                }

                // 监听输入框的变化并保存到 localStorage
                input.addEventListener('input', function () {
                    if (input.id != 'fileInput'){ 
                        localStorage.setItem(input.id, input.value);
                    }
                }); 
            });
        });
        async function submitForm(action) {
            const formData = new FormData(form);

            // Convertir los datos del formulario a un objeto JSON
            const jsonData = {};
            formData.forEach((value, key) => {
                jsonData[key] = value;
            });
            jsonData['action'] = action;

            try {
                const response = await fetch('/', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(jsonData)
                });

                if (response.ok) {
                    const resultresponse = await response.json();
                    result.innerText = resultresponse.message;
                } else {
                    const errorText = await response.text();
                    result.innerText = errorText;
                    alert('Error: ' + errorText);
                }
            } catch (error) {
                console.error('Error al enviar el formulario:', error);
                result.innerText = 'Ocurrió un error al enviar el formulario.';
                alert('Ocurrió un error al enviar el formulario.');
            }
        }
        function clearCache() {
            // 显示确认对话框
            const userConfirmed = confirm('您确定要清除缓存吗？此操作无法撤销。');

            if (userConfirmed) {
                // 清除 localStorage 中的所有数据
                localStorage.clear();

                // 清空页面上的所有输入框和文本区域
                const inputs = document.querySelectorAll('input, textarea');
                inputs.forEach(input => {
                    input.value = ''; // 清空输入框内容
                });

                // 提示用户缓存已清除
                result.html ='缓存已清除！';
            } else {
                // 用户取消操作
                alert('操作已取消。');
            }
        }  
    </script>
    <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js'></script>
</body>
</html>
"""";
        response.ContentType = "text/html";
        response.StatusCode = 200;
        var htmlBytes = Encoding.UTF8.GetBytes(html);
        response.ContentLength = htmlBytes.Length;
        await response.Body.WriteAsync(htmlBytes, 0, htmlBytes.Length);
    }
    public static Func<SignInWeb, Task<string>>? OnControl { get; set; }

    private static async Task HandleControlRequest(HttpContext httpContext, HttpResponse response)
    {
        try
        {
            using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);
            var requestBody = await reader.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<SignInWeb>(requestBody, optionsIn);

            if (data != null && OnControl != null)
            {
                // 触发回调
                var res =await OnControl.Invoke(data);

                // 创建响应 JSON 对象
                var responseData = new Dictionary<string, object>
                    {
                        { "message", res??"Received and processed successfully" },
                        { "code", 1 },
                    };

                var jsonResponse = JsonSerializer.Serialize(responseData);
                var jsonResponseBytes = Encoding.UTF8.GetBytes(jsonResponse);

                response.ContentLength = jsonResponseBytes.Length;
                await response.Body.WriteAsync(jsonResponseBytes, 0, jsonResponseBytes.Length);
            }
            else
            {
                throw new JsonException("Invalid data");
            }
        }
        catch (JsonException)
        {
            response.StatusCode = 400;
            var errorResponse = Encoding.UTF8.GetBytes("Invalid JSON data");
            response.ContentLength = errorResponse.Length;
            await response.Body.WriteAsync(errorResponse, 0, errorResponse.Length);
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            await response.WriteAsync($"Error: {ex.Message}");
        }
    }

    public static async Task OnHttpRequest(HttpContext httpContext)
    {
        var response = httpContext.Response;

        if (RouteHandlers.TryGetValue(httpContext.Request.Path, out var handler))
        {
            await handler(httpContext, response);
        }
        else
        {
            response.StatusCode = 404;
            var notFoundBytes = Encoding.UTF8.GetBytes("404 - Not Found");
            response.ContentLength = notFoundBytes.Length;
            await response.Body.WriteAsync(notFoundBytes, 0, notFoundBytes.Length);
        }
    }


}
