using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using VotingApp.Application;
using VotingApp.Application.Behaviors;
using VotingApp.Application.Features.Candidates;
using VotingApp.Application.Features.Voters;
using VotingApp.Domain.Context;
using VotingApp.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Use the default property (Pascal) casing.
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VotingAppContext>(options =>
    options.UseInMemoryDatabase(databaseName: "VotingAppDb"));

// Repositories declaration
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Data seeding
var scope = app.Services.CreateScope();
using (scope)
{
    var context = scope.ServiceProvider.GetRequiredService<VotingAppContext>();
    DataSeed.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSpa(spa =>
//{
//    // To learn more about options for serving an Angular SPA from ASP.NET Core,
//    // see https://go.microsoft.com/fwlink/?linkid=864501

//    spa.Options.SourcePath = "ClientApp";

//    if (env.IsDevelopment())
//    {
//        spa.UseAngularCliServer(npmScript: "start");
//    }
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Open");

app.MapControllers();

app.Run();
