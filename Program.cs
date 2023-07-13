using DocDocGo.DAL;
using DocDocGo.Models;
using DocDocGo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using DocDocGo.Repositories.Interfaces;
using DocDocGo.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("HospitalManagementSQLConnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connString)); //we add options to configure our use of SQL server.

builder.Services.AddScoped<IRepository<AppointmentModel>, AppointmentRepository>();
builder.Services.AddScoped<IRepository<PatientModel>, PatientRepository>();
builder.Services.AddScoped<IRepository<PrescriptionModel>, PrescriptionRepository>();
builder.Services.AddScoped<IRepository<ReportModel>, ReportRepository>();

builder.Services.AddIdentity<UserModel, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddRoles<IdentityRole<int>>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Account/Login";
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddRazorPages();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    var roles = new[] { "Administrator", "Staff", "Patient" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();

    string email = "sarah-admin@hospitaltrust.com";
    var user = await userManager.FindByEmailAsync(email);
    if (user != null)
    {
        bool isInRole = await userManager.IsInRoleAsync(user, "Administrator");

        if (!isInRole)
        {
            await userManager.AddToRoleAsync(user, "Administrator");
        }
    }

    string staffemail = "pavel.sanjah-staff@hospitaltrust.com";
    var staffuser = await userManager.FindByEmailAsync(staffemail);

    if(staffuser !=null)
    {
        bool isInRole = await userManager.IsInRoleAsync(staffuser, "Staff");

        if (!isInRole)
        {
            await userManager.AddToRoleAsync(staffuser, "Staff");
        }
    }
}

app.MapRazorPages();

app.Run();

