using CRUDDemo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL bağlantı dizesini al
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// AppDbContext'i PostgreSQL ile kullanacak şekilde yapılandır
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// CORS politikasını tanımla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Swagger (OpenAPI) servisini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger Middleware
app.UseSwagger();
app.UseSwaggerUI();

// CORS Middleware
app.UseCors("AllowAll");

// Yetkilendirme ve kimlik doğrulama
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Controller'ları kullanıma sun

app.Run();
