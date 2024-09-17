using Digital_Product_Catalogue.Data;
using Digital_Product_Catalogue.Models;
using Digital_Product_Catalogue.ServiceContract;
using Digital_Product_Catalogue.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();



// Db service 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});


// // Service to Enable to identity Based User
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, int>>();


// Authentication and Authentication and Authorization Service
builder.Services.AddAuthorization(option =>
{
    option.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    option.AddPolicy("NotAuthorized", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return !context.User.Identity.IsAuthenticated;
        });
    });
});

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Account/Login";
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// PDF Generator Setup
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Product}/{action=AddProduct}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
