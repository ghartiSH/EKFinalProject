using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using ServiceLayer.EmployeeService;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

//adding database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.
    UseNpgsql(configuration.GetConnectionString("AppDB"))
    .UseSnakeCaseNamingConvention());


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmployeeService, EmployeeService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
