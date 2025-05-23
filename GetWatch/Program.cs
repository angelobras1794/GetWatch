using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using GetWatch.Services.Db;
using GetWatch.Services;
using GetWatch.Services.User;
using GetWatch.Interfaces.User;
using GetWatch.Interfaces.Movies;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Movies;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.ShoppingCart;
using GetWatch.Services.Tickets;
using GetWatch.Interfaces.SupportTickets;
using Microsoft.AspNetCore.Authentication.Cookies; // Add this at the top
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using GetWatch.Interfaces.Compra;
using GetWatch.Interfaces.Mediator;
using GetWatch.Services.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



builder.Services.AddScoped<IShoppingCart, ShoppingCart>();
builder.Services.AddScoped<ProtectedSessionStorage>();



builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<GetWatchContext>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<UserLoginService>();
builder.Services.AddScoped<UserCreationService>();

builder.Services.AddScoped<ICartItemMapper, CartItemMapper>();
builder.Services.AddScoped<IShoppingCartMapper, ShoppingCartMapper>();
builder.Services.AddScoped<ISupportTicketMapper, SupportTicketMapper>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

builder.Services.AddScoped<ICartItemFactory, CartItemFactory>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ICommandManager, CommandManager>();
builder.Services.AddScoped<IGetWatchMediator,GetWatchMediator>();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; 
    });



builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization(); 


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
