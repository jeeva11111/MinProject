using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinProject.Controllers.Objects;
using MinProject.Data;
using MinProject.Functions.AccountFunctions;
using MinProject.Services;
using MinProject.SignalRFun;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerLink"))
);

// Add authentication services 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];

    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new ArgumentNullException(nameof(jwtKey), "JWT Key cannot be null or empty");
    }

    if (string.IsNullOrEmpty(jwtIssuer))
    {
        throw new ArgumentNullException(nameof(jwtIssuer), "JWT Issuer cannot be null or empty");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Add SignalR and other services
builder.Services.AddSignalR();
builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddTransient<IFunctions, Functions>();
builder.Services.AddScoped<IStudentInterface, StudentRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemServies, ItemServies>();
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
app.MapHub<ChatHub>("/ChatHub");

app.Run();
