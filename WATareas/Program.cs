using Datos;
using Negocio;
using WATareas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDIDatos(builder.Configuration);
builder.Services.AddDINegocio();
builder.Services.AddDIAPI();

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
