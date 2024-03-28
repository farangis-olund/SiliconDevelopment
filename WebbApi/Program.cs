using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => 
x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.RegisterSwagger();
builder.Services.RegistrateJwt(builder.Configuration);

builder.Services.AddScoped<SubscriptionRepository>();
builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ApiUserRepository>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<ContactRepository>();
builder.Services.AddScoped<ServiceRepository>();
builder.Services.AddScoped<ContactService>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Silicon WebApi v1"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
