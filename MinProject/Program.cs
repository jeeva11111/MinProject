using Microsoft.EntityFrameworkCore;
using MinProject.Controllers.Objects;
using MinProject.Data;
using MinProject.Functions.AccountFunctions;
using MinProject.Services;
using MinProject.SignalRFun;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServerLink")));

// Add SignalR  PresenceTracker
builder.Services.AddSignalR();
builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddTransient<IFunctions, Functions>();

builder.Services.AddScoped<IStudentInterface, StudentRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemServies, ItemServies>();
//builder.Services.AddScoped<ConnectionMapping>();
builder.Services.AddHttpContextAccessor();



builder.Services.AddSession(x =>
{
    x.IOTimeout = TimeSpan.FromMinutes(3);
    x.Cookie.Name = "Logger";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

// Map the SignalR hub
//app.MapHub<NotificationHub>("NotificationHub");

app.MapHub<ChatHub>("/ChatHub");


app.Run();
