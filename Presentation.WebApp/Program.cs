using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Helpers;
using Infrastructure.Entities;
using Presentation.WebApp.Helpers;
using Presentation.WebApp.Services;

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
builder.Services.AddScoped<UserCourseRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<ServiceRepository>();

builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CourseRepository>();

builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<FeatureItemService>();
builder.Services.AddScoped<ShowcaseService>();
builder.Services.AddScoped<SwitchService>();
builder.Services.AddScoped<TaskMasterService>();
builder.Services.AddScoped<DownloadAppService>();
builder.Services.AddScoped<IntegrationService>();
builder.Services.AddScoped<SubscriptionSectionService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<UserCourseService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<FormatReviews>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<ApiCourseService>();
builder.Services.AddScoped<ApiSubscribeService>();
builder.Services.AddScoped<AccountService>();

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

builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "1042689527093-td0lbn8c93e52u562eka48n8djj40ui7.apps.googleusercontent.com";
    x.ClientSecret = "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";

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
