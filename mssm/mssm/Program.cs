using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using mssm.Context;
using mssm.Models.TeamModel;
using mssm.Services.IAuth;
using mssm.Services.IProject;
using mssm.Services.ISearch;
using mssm.Services.ITask;
using mssm.Services.ITeam;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
 
builder.Services.AddDefaultIdentity<UserContext>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<_IAuth, _Auth>();
builder.Services.AddTransient<_ITeam, _Team>();
builder.Services.AddTransient<_IProject, _Project>();
builder.Services.AddTransient<_ITask, _Task>();
builder.Services.AddTransient<_ISearch, _Search>();
// Настройка перенаправления на страницу входа
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
});
var app = builder.Build();
app.UseSession();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles(); 
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=task}/{id?}");
app.Run();
