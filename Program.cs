using gacha.Database;
using gacha.Services;
using pd.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do Entity Framework Core com SQLite e o Swagger para documenta��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SerieService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<CollectionService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<SubgenreService>();
builder.Services.AddScoped<SerieService>();
builder.Services.AddScoped<GachaDrawService>();

// configura��es do servidor como CORS e Kestrel
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Any, 80); // Ouvindo em todas as interfaces de rede
});

builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();

// Isso aqui � do banco, vai ter que criar o banco de dados se n�o existir
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// A documenta��o Swagger � configurada para ser exibida em ambientes de desenvolvimento e produ��o
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.MapControllers();

app.Run();