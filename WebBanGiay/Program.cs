using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Areas.Admins.Repository;
using WebBanGiay.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure DbContext with connection string
builder.Services.AddDbContext<DbwebGiayOnlineContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Logging.AddConsole(); // Nếu sử dụng console logging
builder.Logging.AddDebug(); // Thêm debug logging

// Đăng ký services
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddLogging();
// Cấu hình các dịch vụ
builder.Services.AddControllersWithViews();

// Đăng ký dịch vụ IEmailSender
builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    //options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddIdentity<AppUserModel,IdentityRole>()
	.AddEntityFrameworkStores<DbwebGiayOnlineContext>().AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings.
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;

});
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Account/Login"; // Trang đăng nhập
	
	options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Thời gian hết hạn cookie
	options.SlidingExpiration = true; // Gia hạn cookie nếu người dùng hoạt động
	options.LogoutPath = "/Account/Logout"; // Trang đăng xuất
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Routing configuration for Areas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default routing configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
