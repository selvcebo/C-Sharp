using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TechNova
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Añadir Razor Pages y configurar páginas Identity públicas
            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    // Páginas Identity que deben ser públicas
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Login");
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Register");
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/AccessDenied");
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/ConfirmEmail");
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/ForgotPassword");
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/ResetPassword");
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/ExternalLogin");
                    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/RegisterConfirmation");
                });

            builder.Services.AddDbContext<TiendaTechNovaDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Habilitar roles en Identity y permitir login sin confirmación (útil en desarrollo)
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoles<IdentityRole>() // <-- necesario para roles
                .AddEntityFrameworkStores<TiendaTechNovaDbContext>();

            // Política por defecto: requiere usuario autenticado Y rol Admin.
            // Las páginas y acciones con [AllowAnonymous] quedarán accesibles (Home, Login, Register, etc.).
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole("Admin")
                    .Build();
            });

            var app = builder.Build();

            // Seed roles y admin en Desarrollo (lee credenciales desde appsettings.Development.json o user-secrets)
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    CreateRolesAndAdminIfNeededAsync(services, app.Configuration).GetAwaiter().GetResult();

                    // Confirmar emails existentes en dev para evitar bloqueos por EmailConfirmed = false
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    ConfirmAllUsersAsync(userManager).GetAwaiter().GetResult();
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Rutas MVC
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Razor Pages (Identity)
            app.MapRazorPages();

            app.Run();
        }

        private static async Task CreateRolesAndAdminIfNeededAsync(IServiceProvider services, IConfiguration config)
        {
            try
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                string[] roleNames = new[] { "Admin", "User" };
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        var r = await roleManager.CreateAsync(new IdentityRole(roleName));
                        Console.WriteLine(r.Succeeded
                            ? $"[Seed] Created role '{roleName}'"
                            : $"[Seed] Failed creating role '{roleName}': {string.Join(", ", r.Errors.Select(e => e.Description))}");
                    }       
                }

                // Leer credenciales desde configuración (appsettings.Development.json o user-secrets)
                var adminEmail = config["AdminUser:Email"] ?? "admin@local.test";
                var adminPassword = config["AdminUser:Password"] ?? "Admin123!";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"[Seed] Created admin user '{adminEmail}'");
                    }
                    else
                    {
                        Console.WriteLine($"[Seed] Failed creating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        Console.WriteLine("[Seed] Comprueba que la contraseña cumple los requisitos de Identity.");
                    }
                }
                else
                {
                    Console.WriteLine($"[Seed] Admin user '{adminEmail}' already exists.");
                }

                // Asegurar que el usuario admin esté en rol Admin
                if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    var addRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine(addRoleResult.Succeeded
                        ? $"[Seed] Added user '{adminEmail}' to role 'Admin'"
                        : $"[Seed] Failed adding user to role: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Seed] Error: {ex}");
            }
        }

        private static async Task ConfirmAllUsersAsync(UserManager<IdentityUser> userManager)
        {
            try
            {
                var users = userManager.Users.ToList();
                foreach (var user in users)
                {
                    if (!await userManager.IsEmailConfirmedAsync(user))
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var res = await userManager.ConfirmEmailAsync(user, token);
                        Console.WriteLine(res.Succeeded
                            ? $"[Seed] Confirmed email for user {user.Email} (Id: {user.Id})"
                            : $"[Seed] Failed confirming email for {user.Email}: {string.Join(", ", res.Errors.Select(e => e.Description))}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Seed] Error confirming users: {ex}");
            }
        }
    }
}
