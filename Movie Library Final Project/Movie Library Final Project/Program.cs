using MovieLibrary.BL.CommandHandlers.MovieCommandHandlers;
using MediatR;
using Movie_Library_Final_Project.Extensions;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Movie_Library_Final_Project.Middleware;
using Movie_Library_Final_Project.HealthChecks;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);


// Add services to the container.
builder.Services
    .RegisterRepositories()
    .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server");

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

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.Run();
