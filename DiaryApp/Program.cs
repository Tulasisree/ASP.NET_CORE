using DiaryApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//registers service to container that is adding our dbcontext with our appnDBcontext 
// options is created by the AddDbContext() method and passed into your lambda function.
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//directs requests to appropriate controllers and actions
app.UseRouting();

//verifies that users have permission to acess requested resources
app.UseAuthorization();
//serves static files
app.MapStaticAssets();

//defines url patterns and maps them to controllers and actions
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//starts web server and begins listening for http requests
app.Run();
