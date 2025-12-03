using Taxas.API;
using Taxas.API.Servicos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<TaxaDomain>();

// --- CONFIGURAÇÃO DAS INTEGRAÇÕES ---

// 1. Cliente para RESIDÊNCIAS (Coloque a URL da Residencias.API)
// Exemplo: https://localhost:7200/ (Verifique a sua!)
builder.Services.AddHttpClient<ResidenciaClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7060/");
});

// 2. Cliente para MORADORES (Coloque a URL da Moradores.API)
// Exemplo: https://localhost:7042/ (Verifique a sua!)
builder.Services.AddHttpClient<MoradorIntegrationClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7042/");
});

// ------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();