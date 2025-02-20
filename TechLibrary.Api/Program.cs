using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TechLibrary.Api.Filters;
using TechLibrary.Api.Infrastructure.Security.Tokens.Access;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Api.UseCases.Login.DoLogin;
using TechLibrary.Api.UseCases.Book.Filter;
using System.Text;
using Microsoft.OpenApi.Models;

const string AUTHENTICATION_TYPE = "Bearer";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below;
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AUTHENTICATION_TYPE
    });

    builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var signingKey = builder.Configuration["Jwt:SigningKey"]
                             ?? throw new ArgumentNullException("JWT signing key is not configured");

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(signingKey)
                )
            };
        });

    builder.Services.AddScoped<JwtTokenGenerator>();
    builder.Services.AddScoped<RegisterUserUseCase>();
    builder.Services.AddScoped<DoLoginUseCase>();
    builder.Services.AddScoped<FilterBookUseCase>();


    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechLibrary API v1");
            c.RoutePrefix = string.Empty;
            c.ConfigObject.AdditionalItems["version"] = DateTime.Now.Ticks.ToString();
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
});
