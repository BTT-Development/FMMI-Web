using Blazored.Modal;
using FMMI_Domain;
using FMMI_Service;
using FMMI_Web.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazoredModal();

builder.AppendServiceConfiguration();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));


builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

builder.Services.AddCascadingAuthenticationState();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<FMMIContext>(options =>
    options.UseNpgsql(connectionString));

#region Sessions

builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion

builder.AppendServiceConfiguration();

var app = builder.Build();

#region Syncfusion Key
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF5cXGRCf1JpRGFGfV5ycUVCallQTnJXUiweQnxTdEBiWX5dcHBQRGVUUUR3WEleYg==");
#endregion

#region Create database schema if not exists
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<FMMIContext>();
//    db.Database.Migrate(); // 
//}
#endregion

app.MapGet("/login", async (HttpContext context) =>
{
    var redirectUri = "/";
    await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = redirectUri
    });
});

app.MapGet("/logout", async (HttpContext context) =>
{
    var callbackUrl = "/";
    await context.SignOutAsync("Cookies");
    await context.SignOutAsync("OpenIdConnect", new AuthenticationProperties
    {
        RedirectUri = callbackUrl
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
