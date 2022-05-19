using Microsoft.EntityFrameworkCore;
using SmartCareer.DBContext;
using SmartCareer.Models;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(feature =>
                feature.AddPolicy(
                    "CorsPolicy",
                    apiPolicy => apiPolicy
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                ));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SmartCareerDBContext>();

builder.Services.AddControllers().AddFluentValidation(s =>
{
    s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
    s.RegisterValidatorsFromAssemblyContaining<StartupBase>();
});

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMainService, MainService>();

//Validator
builder.Services.AddScoped<IValidator<UserRequest>, UserRequestValidator>();

builder.Services.AddDbContext<SmartCareerDBContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();