using Microsoft.OpenApi.Models;
using CRUDDemo.Abstract;
using CRUDDemo.Data;
using CRUDDemo.Services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql("Host=localhost;Port=5432;Database=crudDemoDb;Username=postgres;Password=ddrylmz")); // DB bağlantısı

                    services.AddControllers();   
                    services.AddScoped<IEmployeeService, EmployeeService>(); // Servis kaydı

                    // Swagger 
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo 
                        { 
                            Title = "CRUD API", 
                            Version = "v1",
                            Description = "Employee CRUD işlemleri için API"
                        });
                    });
                });

                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    
                    // Swagger Middleware 
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD API v1");
                    });

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
