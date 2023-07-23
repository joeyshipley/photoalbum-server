using Microsoft.OpenApi.Models;
using API.Infrastructure.IoC;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyRegistrations();
builder.Services.AddDbContext<MigrationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging()
);

const string POLICY_NAME = "CORS_POLICY";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: POLICY_NAME, policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen((c) =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LT Photo Album API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

var appContext = builder.Services.Resolve<IMigrationContext>();
await appContext.Migrate();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(POLICY_NAME);

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "";
});

app.MapControllers();
app.Run();
