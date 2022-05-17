using q5id.platform.email.dal.Repositories;
using q5id.platform.email.dal.Interfaces;
using q5id.platform.email.dal;
using Microsoft.EntityFrameworkCore;
using q5id.platform.email.api.Interfaces;
using q5id.platform.email.api.Services;
using Serilog;
using Serilog.Sinks.File;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependency injection
builder.Services.AddDbContextPool<EmailContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("EmailApiContext");
    options.UseSqlServer(connectionString);
});

// Remove DBPool for local debugging 
//builder.Services.AddDbContext<EmailContext>(options => {
//    string connectionString = builder.Configuration.GetConnectionString("EmailApiContext");
//    options.UseSqlServer(connectionString);
//});

builder.Services.AddScoped<IEmailRepository, EmailRepository>();

builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
builder.Services.AddTransient<IAppVersionService, AppVersionService>();

// builder.WebHost.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

