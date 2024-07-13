
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FileService.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenCustom(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup =>
        {


            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "FileService.API", Version = "v1" });


            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    jwtSecurityScheme, Array.Empty<string>() 
                }
            });

        });

        // Configure Swagger UI
        services.Configure<SwaggerUIOptions>(c =>
        {
            //c.RoutePrefix = string.Empty;
            c.DocExpansion(DocExpansion.None); // Collapse all controllers by default

        });
        return services;
    }
}

