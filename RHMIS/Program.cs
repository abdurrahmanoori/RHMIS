using FluentValidation.AspNetCore;
using PHMIS.Extensions;
using PHMIS.Infrastructure.Extensions;
using PHMIS.Application.Extensions;
using PHMIS.Infrastructure.Context;
using PHMIS.Identity.Extensions;

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
    client.BaseAddress = new Uri("https://localhost:7216");
});

builder.Services.AddReactAppCors();

builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureIdentityServices<AppDbContext>(builder.Configuration);

// MiniProfiler registration
builder.Services.AddMiniProfilerServices();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.UseSwaggerWithUI(app.Environment);

// MiniProfiler middleware
app.UseMiniProfiler();
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
