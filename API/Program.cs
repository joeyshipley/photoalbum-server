using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((c) =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LT Photo Album API", Version = "v1" });
});

//httpConfiguration
//     .EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
//     .EnableSwaggerUi();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "";
});

app.MapControllers();
app.Run();