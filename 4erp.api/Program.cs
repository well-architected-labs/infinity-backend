using System.Text;
using _4erp.api.entities;
using _4erp.api.entities.candidature;
using _4erp.api.entities.ocupation;
using _4erp.api.entities.skill;
using _4erp.api.entities.status;
using _4erp.api.entities.vacancy;
using _4erp.application.inbound;
using _4erp.application.Inbound.Authorization;
using _4erp.application.Inbound.Users;
using _4erp.application.services;
using _4erp.application.services.Candidatures;
using _4erp.domain.Ports;
using _4erp.domain.repositories;
using _4erp.domain.repositories.Candidates;
using _4erp.domain.Services;
using _4erp.domain.Services.Candidatures;
using _4erp.domain.Services.Tenant;
using _4erp.domain.Services.Users;
using _4erp.domain.Services.Vacancies;
using _4erp.infrastructure.data.context;
using _4erp.infrastructure.Repositories;
using _4erp.infrastructure.Repositories.Users;
using _4erp.infrastructure.Repositories.Vacancies;
using _4erp.infrastructure.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenericRepository<Status>, GenericRepository<Status>>();
builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
builder.Services.AddScoped<IGenericRepository<Skill>, GenericRepository<Skill>>();
builder.Services.AddScoped<IGenericRepository<Ocupation>, GenericRepository<Ocupation>>();
builder.Services.AddScoped<IGenericRepository<Vacancy>, GenericRepository<Vacancy>>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<Candidature>, GenericRepository<Candidature>>();
builder.Services.AddScoped<IGenericService<Status>, GenericService<Status>>();
builder.Services.AddScoped<IGenericService<Ocupation>, GenericService<Ocupation>>();
builder.Services.AddScoped<IGenericService<Vacancy>, GenericService<Vacancy>>();
builder.Services.AddScoped<IGenericService<Role>, GenericService<Role>>();
builder.Services.AddScoped<IGenericService<Skill>, GenericService<Skill>>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IGenericService<Candidature>, GenericService<Candidature>>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidatureService, CandidatureService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddHttpContextAccessor();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
        policy.WithOrigins("https://app.infinityteam.cloud")
        .AllowAnyHeader()
        .AllowAnyMethod();
        policy.WithOrigins("https://app.infinityteam.cloud")
        .AllowAnyHeader()
        .AllowAnyMethod();
        
    });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("4erp.infrastructure")
    )
);

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JWTSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "4erp.ai",
            ValidAudience = "4erp.ai",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings is not null ? jwtSettings.Secret : ""))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "4ERP",
        Version = "v1",
        Description = "API for 4ERP",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Gabriel Borges",
            Email = "contato@4erp.io",
            Url = new Uri("https://4erp.io")
        }
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
        options.RoutePrefix = "swagger";
    });
}


app.UseHttpsRedirection();


app.UseCors("AllowAllOrigins");


app.UseAuthorization();
app.MapControllers();
app.Run();
