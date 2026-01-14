using BusinessLogic;
using Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();
string connStr;

var isDevelopment = builder.Environment.IsDevelopment();

connStr = builder.Environment.IsDevelopment() ? Environment.GetEnvironmentVariable("DefaultConnection") :
    Environment.GetEnvironmentVariable("SqlConnectionString");

builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseSqlServer(connStr));
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IProductsDbContext, ProductsDbContext>();


builder.Build().Run();

