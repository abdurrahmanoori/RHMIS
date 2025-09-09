using FluentValidation.AspNetCore;
using RHMIS.Extentions;
using RHMIS.Infrastructure.Extentions;
using RHMIS.Application.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogService();

// Add services to the container.
builder.Services.AddControllers()
    .AddControllersAsServices();


//builder.Services.AddCustomModelStateValidation();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7016");
});

builder.Services.AddReactAppCors();

builder.Services.AddSwaggerGen();

builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
//builder.Services.ConfigurePresentionService();

// MiniProfiler registration
builder.Services.AddMiniProfilerServices();






// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.UseSwaggerWithUI(app.Environment);

// MiniProfiler middleware
app.UseMiniProfiler();
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
