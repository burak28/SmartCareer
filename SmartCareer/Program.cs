using SmartCareer.DBContext;
using SmartCareer.Models;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

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

//Validator
builder.Services.AddScoped<IValidator<UserRequest>, UserRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
