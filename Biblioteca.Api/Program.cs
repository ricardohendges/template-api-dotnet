using Biblioteca.Api.Endpoints;
using Biblioteca.Aplicacao;
using Biblioteca.Infraestrutura;

// RAIZ DE COMPOSIÇÃO: liga as peças e mais nada.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicacao();
builder.Services.AddInfraestrutura();

var app = builder.Build();

app.MapGet("/", () => "API Biblioteca no ar!");
app.MapLivrosEndpoints();
app.MapUsuariosEndpoints();

app.Run("http://localhost:5000");
