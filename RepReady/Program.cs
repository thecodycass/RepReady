using Microsoft.AspNetCore.Components;
using RepReady.Components;
using RepReady.Configuration;
using RepReady.Controllers.MinimalAPI;
using RepReady.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseStaticWebAssets();
}

// Configure Supabase settings
builder.Services.Configure<SupabaseSettings>(
    builder.Configuration.GetSection("Supabase"));
// Register Supabase service
builder.Services.AddSingleton<SupabaseService>();

// Register OpenAPI
builder.Services.AddOpenApi();

// Add HttpClient for API calls using IHttpClientFactory
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:8080"); // Docker Compose Environment
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Initialize Supabase client
var supabaseService = app.Services.GetRequiredService<SupabaseService>();
await supabaseService.InitializeAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler("/Error", createScopeForErrors: true);
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseAntiforgery();

app.UseAuthorization();

// API Endpoints
app.MapUserEndpoints();
app.MapExerciseEndpoints();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
