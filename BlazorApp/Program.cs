using BlazorApp.Components;
using BlazorApp.Components.Services;
using BlazorApp.Model;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<TokenState>();

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");
var apiCalendarUrl = builder.Configuration.GetValue<string>("ApiCalendarUrl");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl!)
});

builder.Services.AddHttpClient<IBibleApiService, BibleApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});

builder.Services.AddHttpClient<ICalendarApiService, CalendarApiService>(client =>
{
    client.BaseAddress = new Uri(apiCalendarUrl!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();