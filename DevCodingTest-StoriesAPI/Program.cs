using DevCodingTest_StoriesAPI.Extensions;
using DevCodingTest_StoriesAPI.Services;
using DevCodingTest_StoriesAPI.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IServiceManager, ServiceManager>();

var app = builder.Build();

app.ConfigureExceptionHandler();

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
