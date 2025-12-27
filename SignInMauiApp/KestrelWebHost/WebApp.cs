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
                    if (!IsAuthenticated(ctx)) {
                        await RenderLoginForm(res);
                        return;
                    }
                    if (ctx.Request.Method == "POST") {
                        await HandleSignInControlRequest(ctx, res);
                    } else {
                        await RenderSignInControlForm(res);
                    }
                }
            },
            { "/login", async (ctx, res) =>
                {
                    if (ctx.Request.Method == "POST") {
                        await HandleLoginRequest(ctx, res);
                    } else {
                        await RenderLoginForm(res);
                    }
                }
            },
            { "/logout", async (ctx, res) =>
                { 
                    HandleLogoutRequest(ctx, res); 
                }
            },
            { "/bootstrap.bundle.min.js", StaticFiles},
            { "/bootstrap.min.css", StaticFiles},
        };
    private static bool IsAuthenticated(HttpContext ctx)
    {
        return ctx.Request.Cookies.TryGetValue("auth", out var val) && val == "1";
    }
    private static async Task StaticFiles(HttpContext ctx, HttpResponse response)
    {
        var wwwroot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
        var filePath = ctx.Request.Path.Value?.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        if (string.IsNullOrEmpty(filePath))
        {
            response.StatusCode = 404;
            await response.WriteAsync("File not found");
            return;
        }
        var fullPath = Path.Combine(wwwroot, filePath);

        if (!File.Exists(fullPath))
        {
            response.StatusCode = 404;
            await response.WriteAsync("File not found");
            return;
        }

        var ext = Path.GetExtension(fullPath).ToLowerInvariant();
        response.ContentType = ext switch
        {
            ".js" => "application/javascript",
            ".css" => "text/css",
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".ico" => "image/x-icon",
            ".svg" => "image/svg+xml",
            ".html" => "text/html",
            _ => "application/octet-stream"
        };

        var bytes = await File.ReadAllBytesAsync(fullPath);
        response.StatusCode = 200;
        response.ContentLength = bytes.Length;
        await response.Body.WriteAsync(bytes, 0, bytes.Length);
    }

    private static async Task RenderLoginForm(HttpResponse response)
    {
        var html = """
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Registro de jornada laboral</title>
    <link href='bootstrap.min.css' rel='stylesheet'>
</head>
<body>
    <div class='container mt-5 text-center w-75'>
        <h4>Inicia sesión</h4><br/><br/> 
        <form id='loginForm' class='mt-4'>
            <div class='mb-3'>
                <input type='text' class='form-control' id='username' name='username' placeholder="Usuario" required>
            </div>
            <div class='mb-3'>
                <input type='password' class='form-control' id='password' name='password' placeholder="Contraseña" required>
            </div> 
            <button type='button' class='btn btn-primary w-100' onclick='submitLogin()'>Iniciar sesión</button><br/><br/>
            <h6 id='result' class='text-info'></h6>
            <h6 id='resultError' class='text-danger'></h6>
        </form>
    </div>
    <script>
        const result = document.getElementById('result');
        const resultError = document.getElementById('resultError');
        // 在页面加载时恢复输入框的值
        document.addEventListener('DOMContentLoaded',async function () {
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
            if (localStorage.getItem('username')){
                const jsonData = {}; 
                jsonData['username'] = localStorage.getItem('username');
                jsonData['password'] = localStorage.getItem('password'); 
                jsonData['action'] = 'login';
                submitLoginData(jsonData);
            }
        });
        async function submitLogin() {
            result.innerText ='',
            resultError.innerText ='';
            const form = document.getElementById('loginForm');
            const formData = new FormData(form);
            const jsonData = {};
            formData.forEach((value, key) => {
                jsonData[key] = value;
            });
            jsonData['action'] = 'login';
            await submitLoginData(jsonData);
        }
        async function submitLoginData(jsonData) { 
            try {
                const response = await fetch('/login', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(jsonData)
                });
                if (response.ok) {
                    const resultresponse = await response.json(); 
                    if (resultresponse.Success) {
                        result.innerText = resultresponse.Message;
                        setTimeout(() => { window.location.href = '/'; }, 50);
                    } else { 
                        resultError.innerText = resultresponse.Message;
                    } 
                } else {
                    result.innerText = 'Error de inicio de sesión';
                }
            } catch {
                resultError.innerText = 'Error de red';
            }
        }
    </script>
    <script src='bootstrap.bundle.min.js'></script>
</body>
</html>
""";
        response.ContentType = "text/html";
        response.StatusCode = 200;
        var htmlBytes = Encoding.UTF8.GetBytes(html);
        response.ContentLength = htmlBytes.Length;
        await response.Body.WriteAsync(htmlBytes, 0, htmlBytes.Length);
    }
    private static async Task RenderSignInControlForm(HttpResponse response)
    {
        var html = """
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Registro de jornada laboral</title>
    <link href='bootstrap.min.css' rel='stylesheet'>
    <style>
        .logout-fixed {
            position: fixed; 
            bottom: 10px;
            width: 100%;
            z-index: 999; 
        }
    </style>
</head>
<body>
    <div class='container mt-5 text-center'>
        <h4>Registro de jornada laboral</h4><hr/><br/><br/>
        <h6 id='result' class='mt-3 mb-3 text-success'>Bienvenido</h6>
        <h6 id='resultError' class='mt-3 mb-3 text-danger'></h6><br/><br/> 
        <div class='m-4'>  
            <button type='button' class='btn btn-primary w-75' onclick='submitForm("signin")'>Entrada</button>
        </div>
        <div class='m-4'>
            <button type='button' class='btn btn-success w-75' onclick='submitForm("signout")'>Salida</button>
        </div>
    </div>
    <div class='container logout-fixed text-center'>
        <div class='m-4'>
            <button type='button' class='btn btn-secondary w-75' onclick='logout()'>Cerrar sesión</button>
        </div>
    </div>
    <script>
        const result = document.getElementById('result');
        const resultError = document.getElementById('resultError');
        function getCookie(name) {
            const value = `; ${document.cookie}`;
            const parts = value.split(`; ${name}=`);
            if (parts.length === 2) return parts.pop().split(';').shift();
            return null;
        }
        function deleteCookie(name) {
            document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
        }
        document.addEventListener('DOMContentLoaded',function () { 
            const lastSignIn = getCookie('lastSignIn');
            if (lastSignIn){
                result.innerText = 'Bienvenido,' + localStorage.getItem('username') + ' ' + lastSignIn;
            } else {
                result.innerText = 'Bienvenido,' + localStorage.getItem('username');
            }
        });
        async function submitForm(action) {
            result.innerText ='',
            resultError.innerText ='';
            const jsonData = {};
            jsonData['username'] = localStorage.getItem('username');
            jsonData['password'] = localStorage.getItem('password'); 
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
                    if (resultresponse.Success) {
                        result.innerText = resultresponse.Message;
                    } else { 
                        resultError.innerText = resultresponse.Message;
                    }
                } else {
                    const errorText = await response.text();
                    resultError.innerText = errorText;
                    alert('Error: ' + errorText);
                }
            } catch (error) {
                console.error('Error al enviar el formulario:', error);
                resultError.innerText = 'Ocurrió un error al enviar el formulario.';
                alert('Ocurrió un error al enviar el formulario.');
            }
        }
        function logout() {
            const userConfirmed = confirm('¿Cerrar sesión?');

            if (userConfirmed) { 
                localStorage.clear(); 
                deleteCookie('lastSignIn');
                fetch('/logout', { method: 'POST' })
                    .then(() => {
                        window.location.href = '/';
                    });
                //setTimeout(() => { window.location.href = '/'; }, 500);  
            }
        }  
    </script>
    <script src='bootstrap.bundle.min.js'></script>
</body>
</html>
""";
        response.ContentType = "text/html";
        response.StatusCode = 200;
        var htmlBytes = Encoding.UTF8.GetBytes(html);
        response.ContentLength = htmlBytes.Length;
        await response.Body.WriteAsync(htmlBytes, 0, htmlBytes.Length);
    }

    public static Func<SignInWeb, Task<SignInResponse>>? OnControl { get; set; }

    private static async Task HandleSignInControlRequest(HttpContext httpContext, HttpResponse response)
    {
        try
        {
            using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);
            var requestBody = await reader.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<SignInWeb>(requestBody, optionsIn);

            if (data != null && OnControl != null)
            {
                // 触发回调
                var res = await OnControl.Invoke(data); 
                var jsonResponse = JsonSerializer.Serialize(res);
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

    private static void HandleLogoutRequest(HttpContext httpContext, HttpResponse response)
    {
        response.Cookies.Delete("auth");
        response.Cookies.Delete("lastSignIn");
    }

    private static async Task HandleLoginRequest(HttpContext httpContext, HttpResponse response)
    {
        try
        {
            using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);
            var requestBody = await reader.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<SignInWeb>(requestBody, optionsIn);

            if (data != null && OnControl != null)
            {
                // 触发回调
                var res = await OnControl.Invoke(data);
                var jsonResponse = JsonSerializer.Serialize(res);
                if (!res.Success)
                {
                    response.StatusCode = 401;
                }
                else
                {
                    response.Cookies.Append("auth", "1", new CookieOptions { HttpOnly = true });
                    response.Cookies.Append("lastSignIn", $"{res.LastSignIn}", new CookieOptions { HttpOnly = true });
                }

                var jsonResponseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                response.ContentLength = jsonResponseBytes.Length;
                await response.Body.WriteAsync(jsonResponseBytes, 0, jsonResponseBytes.Length);
            } 
        }
        catch
        {
            response.StatusCode = 400;
            var errorResponse = Encoding.UTF8.GetBytes("Invalid login data");
            response.ContentLength = errorResponse.Length;
            await response.Body.WriteAsync(errorResponse, 0, errorResponse.Length);
        }
    }


}
