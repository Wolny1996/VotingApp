using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VotingApp.Application;
using VotingApp.Application.Behaviors;
using VotingApp.Application.Features.Candidates;
using VotingApp.Application.Features.Voters;
using VotingApp.Domain.Context;
using VotingApp.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VotingAppContext>(options =>
    options.UseInMemoryDatabase(databaseName: "VotingAppDb"));

builder.Services.AddScoped<ICandidatesRepository, CandidatesRepository>();
builder.Services.AddScoped<IVotersRepository, VotersRepository>();

// MediatR library
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly);
});

// FluentValidation
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly, includeInternalTypes: true);

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

app.Run();
