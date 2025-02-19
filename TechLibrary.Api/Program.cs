using TechLibrary.Api.Filters;
using TechLibrary.Api.Infrastructure.Security.Tokens.Access;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Api.UseCases.Login.DoLogin;
using TechLibrary.Api.UseCases.Book.Filter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TechLibrary API",
        Version = "v1"
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

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