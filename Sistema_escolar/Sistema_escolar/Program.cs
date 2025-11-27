using Microsoft.EntityFrameworkCore;
using Sistema_escolar.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<Sistema_escolarContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("Sistema_escolarContext") ?? throw new InvalidOperationException("Connection string 'Sistema_escolarContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Validacion de la Base de Datos
builder.Services.AddDbContext<SistemaEscolarContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SistemaEscolarContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
