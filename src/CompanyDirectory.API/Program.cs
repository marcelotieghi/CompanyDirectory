using Carter;
using CompanyDirectory.CrossCutting.AppDependencies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraLayer(builder.Configuration);
builder.Services.AddUseCasesLayer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();