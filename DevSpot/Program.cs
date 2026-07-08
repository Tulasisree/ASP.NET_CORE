using Data.UserSeeder;
using DevSpot.Data;
using DevSpot.RoleSeeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options=>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>() // add roles for the identity user from prebuilt Identity Role
    .AddEntityFrameworkStores<ApplicationDbContext>();//add stores for entity user and roles to store in ef

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//using service provider which contains all the services of our application
//we use to it to get role manger and usermanager to configured to create and update , patch roles of
// type IdenttityRole
using (var scope = app.Services.CreateScope())
{
    //used to resolve dependencies from the scope
    var services = scope.ServiceProvider;
    //SeedRolesAsync is static so we can directly call it without class instance
    RoleSeeder.SeedRolesAsync(services).Wait();

    UserSeeder.SeedUserAsync(services).Wait();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
