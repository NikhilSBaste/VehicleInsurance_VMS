using VehicleInsuranceProject.Repository;
using Microsoft.EntityFrameworkCore;
using VehicleInsuranceProject.BusinessLogic;
using VehicleInsuranceProject.Repository.Data;
using Microsoft.AspNetCore.Identity;
using VehicleInsuranceProject.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>() // Specify User and Role types
    .AddEntityFrameworkStores<ApplicationDbContext>() // Use EF Core for storage
    .AddDefaultTokenProviders(); // For password reset, email confirmation, etc.

builder.Services.AddDbContext<ClaimDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ClaimInsureContext' not found.")));

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IClaim, ClaimService>();


var app = builder.Build();

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
