using Serilog.Events;
using Serilog;
using static CSharpFunctionalExtensions.Result;
using GetAddressWebAPI.Options;
using GetAddressWebAPI.AddressServiceCommunication;
using GetAddressWebAPI.Features;
using GetAddressWebAPI;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSerilog();

builder.Services.Configure<DaDataOptions>(
            builder.Configuration.GetSection(DaDataOptions.DaData));

builder.Services.AddHttpClient(DaDataOptions.DaDataClient, client =>
{
    var daDataOptions = builder.Configuration.GetSection(DaDataOptions.DaData).Get<DaDataOptions>()
        ?? throw new ApplicationException("Missing DaData configuration");

    client.BaseAddress = new Uri(daDataOptions.Endpoint);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Authorization", $"Token {daDataOptions.AccessKey}");
    client.DefaultRequestHeaders.Add("X-Secret", daDataOptions.SecretKey);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<GetAddressHandler>();

builder.Services.AddCors();

var app = builder.Build();

app.UseExceptionMiddleware();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
