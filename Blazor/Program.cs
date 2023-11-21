using Blazor.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddOpenIdConnect(oidcOption =>
{
    oidcOption.Authority = "https://dev-85040119.okta.com/oauth2/default";
    oidcOption.ClientId = "0oaddv27y1lH3GvDX5d7";
    oidcOption.ClientSecret = "TVuV9-iKjjdhBxkewqBYreRCwhEqKT-FsnWW2N4gpjkvJhAME-OJYyw_VGomhqDe";
    oidcOption.CallbackPath = "/authorization-code/callback";
    oidcOption.ResponseType = "code";
    oidcOption.SaveTokens = true;
    oidcOption.Scope.Add("openid");
    oidcOption.Scope.Add("profile");
    oidcOption.TokenValidationParameters.ValidateIssuer = true;
    oidcOption.TokenValidationParameters.NameClaimType = "name";
}).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
