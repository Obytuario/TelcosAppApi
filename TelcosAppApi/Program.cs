using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TelcosAppApi;

using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);


startup.ConfigureServices(builder.Services);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(builder.Configuration["KeyJwt"])),
                   ClockSkew = TimeSpan.Zero
               });

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

    
