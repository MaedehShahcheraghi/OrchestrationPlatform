using System.Text.Json.Serialization;
using OrchestrationPlatform.Api.Hubs;
using OrchestrationPlatform.Api.Services;
using OrchestrationPlatform.Application.Abstractions.Services.Api;
using OrchestrationPlatform.Application.Abstractions.Services.SeedData;
using OrchestrationPlatform.Application.Extensions;
using OrchestrationPlatform.Infrastructure.External.Extensions;
using OrchestrationPlatform.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IOperationNotifierService, SignalROperationNotifierService>();

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


using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IApplicationDbSeeder>();
    await seeder.SeedAsync();
}

app.MapHub<OperationHub>("/hubs/operation");
app.Run();