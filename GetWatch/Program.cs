using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using GetWatch.Data;
using GetWatch.Services.Db;
using GetWatch.Services;
using GetWatch.Interfaces.Movies;
using GetWatch.Services.Movies;
using Microsoft.AspNetCore.Authentication.Cookies; // Add this at the top
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



builder.Services.AddScoped<IShoppingCart, ShoppingCart>();
builder.Services.AddScoped<ProtectedSessionStorage>();



builder.Services.AddScoped<UserCreationService>();
builder.Services.AddDbContext<GetWatchContext>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<UserLoginService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Redirect here if unauthenticated
    });



builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();



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

app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();  // Enable authorization middleware


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
