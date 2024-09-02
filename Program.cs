using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PeopleApi.Authorization;
using PeopleApi.Data;
using PeopleApi.Models;
using PeopleApi.Services.Usuarios;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FluentValidation;
using PeopleApi.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connString = builder.Configuration.GetConnectionString("ConexaoSql");
var key = Encoding.ASCII.GetBytes(builder.Configuration["jwt:Key"]);        // Key   <-->    secretKey

//var key = Encoding.ASCII.GetBytes(builder.Configuration["jwt:secretKey"]);

builder.Services.AddDbContext<PessoaDbContext>
    (opts =>
    {
        opts.UseSqlServer(connString);
    });

builder.Services.AddDbContext<UsuarioDbContext>
    (opts =>
    {
        opts.UseSqlServer (connString);
    });

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

// #####    O importante é passar um tipo que todos os validadores estarão lá...        ###################
builder.Services.AddValidatorsFromAssemblyContaining<CreatePessoaValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(i =>
//{
//    i.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "PeopleApi",
//        Version = "v1",
//        Description = "O objetivo deste documento é trazer informações relevantes ao Back-end quanto uso desta " +
//        "API desenvolvida em .NET 6, implementando autenticação Jwt, para que posteriormente possa ser consumida no Front-end People-Web-App (implementação futura...)."
//    });
//});



builder.Services.AddSwaggerGen(i =>
{
    i.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PeopleApi",
        Version = "v1.0",
        Description = "O objetivo deste documento é trazer informações relevantes ao Back-end quanto uso desta " +
        "API desenvolvida em .NET 6, implementando autenticação Jwt, para que posteriormente possa ser consumida no Front-end People-Web-App (implementação futura...)."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    i.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy =>
         policy.AddRequirements(new IdadeMinima(18))
    );
});

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

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
