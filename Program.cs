using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using southSoundWebsite;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddMvc();
var connectionString = builder.Configuration["ConnectionString:SouthSoundSql"];
builder.Services.AddMemoryCache();
builder.Services.AddSession();  
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });   
builder.Services.AddDbContext<reportsContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

