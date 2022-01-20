using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using southSoundWebsite;
using southSoundWebsite.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
var connectionStringDb = builder.Configuration["ConnectionString:SouthSoundSql"];
var connectionStringUsers = builder.Configuration["ConnectionString:Users"];
builder.Services.AddDbContext<reportsContext>(options => options.UseSqlServer(connectionStringDb));
builder.Services.AddDbContext<UsersContext>(options => options.UseSqlServer(connectionStringUsers));
builder.Services.AddMemoryCache();
builder.Services.AddSession();  
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });   

builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();   
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.Run();

