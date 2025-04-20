using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using GetWatch.Data;
using GetWatch.Services.Db;
using GetWatch.Services;
using GetWatch.Interfaces.Movies;
using GetWatch.Services.Movies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<UserCreationService>();
builder.Services.AddDbContext<GetWatchContext>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
