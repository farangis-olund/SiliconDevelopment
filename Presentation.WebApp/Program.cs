using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Helpers;
using Infrastructure.Entities;
using Presentation.WebApp.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<FeatureRepository>();
builder.Services.AddScoped<FeatureItemRepository>();
builder.Services.AddScoped<ShowcaseRepository>();
builder.Services.AddScoped<SwitchRepository>();
builder.Services.AddScoped<TaskMasterRepository>();
builder.Services.AddScoped<DownloadAppRepository>();
builder.Services.AddScoped<AppPlatformRepository>();
builder.Services.AddScoped<IntegrationRepository>();
builder.Services.AddScoped<SubscriptionSectionRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AddressRepository>();

builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<FeatureItemService>();
builder.Services.AddScoped<ShowcaseService>();
builder.Services.AddScoped<SwitchService>();
builder.Services.AddScoped<TaskMasterService>();
builder.Services.AddScoped<DownloadAppService>();
builder.Services.AddScoped<IntegrationService>();
builder.Services.AddScoped<SubscriptionSectionService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<FormatReviews>();

builder.Services.AddDefaultIdentity<UserEntity>(x => { 
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<DataContext>() ;

builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.HttpOnly= true;
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
});

builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "2959151207556497";
    x.AppSecret = "46e72e1affb7b4d0cf8e7e0eca1f4537";
    x.Fields.Add("first_name");
	x.Fields.Add("last_name");

});

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseUserSessionValidation();
app.UseAuthorization();
app.UseMiddleware<BreadcrumbMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
