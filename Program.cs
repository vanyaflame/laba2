using Microsoft.EntityFrameworkCore;
using SimpleTaskApp.Models;
using SimpleTaskApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация подключения к БД
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=db;Database=taskdb;Username=postgres;Password=YourStrongPassword123";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Создание БД при первом запуске
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();