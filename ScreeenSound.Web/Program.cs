using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using ScreeenSound.Web.Components;
using ScreeenSound.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Adiciona HttpClient com base URL definida em appsettings.json
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Serviço de API customizado (usando IHttpClientFactory)
builder.Services.AddTransient<ArtistaAPI>();
builder.Services.AddMudServices();
var app = builder.Build();

// Pipeline de requisição
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Mapeamento do componente raiz
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
