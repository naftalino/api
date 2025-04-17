using gacha.Database;
using gacha.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SerieService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<CollectionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();