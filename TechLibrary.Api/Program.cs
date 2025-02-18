using TechLibrary.Api.Filters;
using TechLibrary.Api.Infrastructure.Security.Tokens.Access;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Api.UseCases.Login.DoLogin;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<DoLoginUseCase>();

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