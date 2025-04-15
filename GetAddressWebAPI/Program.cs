using Serilog.Events;
using Serilog;
using static CSharpFunctionalExtensions.Result;
using GetAddressWebAPI.Options;
using GetAddressWebAPI.AddressServiceCommunication;
using GetAddressWebAPI.Features;
using GetAddressWebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDaDataService(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddHttpContextAccessor();

builder.Services.AddHandlers();

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
