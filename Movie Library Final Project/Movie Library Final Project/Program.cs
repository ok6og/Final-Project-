using FluentValidation;
using FluentValidation.AspNetCore;
using Kafka.KafkaConfig;
using MediatR;
using Movie_Library_Final_Project.Extensions;
using Movie_Library_Final_Project.HealthChecks;
using Movie_Library_Final_Project.Middleware;
using MovieLibrary.BL.CommandHandlers.MovieCommandHandlers;
using MovieLibrary.Models.MongoDbModels;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);

builder.Services.Configure<MyKafkaSettings>(
    builder.Configuration.GetSection(nameof(MyKafkaSettings)));
builder.Services.Configure<List<MyKafkaSettings>>(
    builder.Configuration.GetSection(nameof(MyKafkaSettings)));
builder.Services.Configure<MongoDbModel>(
    builder.Configuration.GetSection(nameof(MongoDbModel)));

// Add services to the container.
builder.Services
    .RegisterRepositories()
    .RegisterServices()
    .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<MongoHealthCheck>("MongoDBConnectionCheck");

builder.Services.AddMediatR(typeof(AddMovieCommandHandler).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.RegisterHealthChecks();
app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.Run();
