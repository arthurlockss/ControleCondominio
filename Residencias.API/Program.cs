using Residencias.API;
using Residencias.API.Servicos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();

builder.Services.AddHttpClient<MoradorClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7042/");
});

builder.Services.AddScoped<ResidenciaDomain>();

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