using CRUDDemo.Abstract;
using CRUDDemo.Data;
using CRUDDemo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                        options.UseNpgsql("Host=localhost;Port=5432;Database=crudDemoDb;Username=postgres;Password=ddrylmz")); // Use your database connection string here

                    services.AddControllers();   
                    services.AddScoped<IEmployeeService, EmployeeService>(); // Register the service
                });

                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
