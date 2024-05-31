using Microsoft.EntityFrameworkCore;
using MinProject.Data;
using MinProject.Functions.AccountFunctions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();

// DbContext 

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServerLink")));



builder.Services.AddTransient<IFunctions, Functions>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(x => { x.IOTimeout = TimeSpan.FromMinutes(3); x.Cookie.Name = "Logger"; });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
