using System.Text;
using Application.UnitOfWork;
using Domain.Interfaces;


namespace API.Extension;

public static class AplicationServicesExtensions
{

    
    public static void ConfigureCors(this IServiceCollection services) =>
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()    //WithOrigins("https://domini.com")
        .AllowAnyMethod()           //WithMethods(*GET", "POST")
        .AllowAnyHeader());         //WithHeaders(*accept*, "content-type")
    });

        
    public static void AddApplicationServices(this IServiceCollection services)
    {        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    
    
}


