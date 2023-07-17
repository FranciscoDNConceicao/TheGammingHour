using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Globalization;
using UTAD.LAB._2022.TheGammingHour.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddDbContext<TheGammingHourDatabase>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TheGammingHourDatabase")));
builder.Services.AddTransient<DefinicaoSistema>();

var app = builder.Build();
using var scope = app.Services.CreateScope();

using (var _scope = app.Services.CreateScope())
{
    var RoleManager = _scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    SeedRoles.Seed(RoleManager);
}
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
var services = scope.ServiceProvider;
var definicoes = services.GetRequiredService<DefinicaoSistema>();
definicoes.CreateGroupsMenu();
definicoes.CreateCategories();
definicoes.CreateDefaultGames();
definicoes.AddProdutoraPlataform();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
