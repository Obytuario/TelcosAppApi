﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TelcosAppApi.DI;
using TelcosAppApi.DataAccess.DataAccess;
using System.Reflection;

namespace TelcosAppApi
{
    public class Startup
    {
        #region Properties
        private const string _DBSETTING = "DbSetting";
        private const string _NAMECORSPOLICY = "MyAllowSpecificOrigins";
        #endregion Properties

        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            //services.AddDbContext<TelcosApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TelcosConnectionString")));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            _ConfigCorsPolicy(services);
            _ConfigOthers(services);
            _ConfigSQL(services);

        }
        private void _ConfigCorsPolicy(IServiceCollection services)
        {
            services.AddCors(opt => {
                opt.AddPolicy(name: _NAMECORSPOLICY, o => {
                    o.AllowAnyHeader();
                    o.AllowAnyMethod();
                    o.AllowAnyOrigin();
                });
            });
        }

        private void _ConfigSQL(IServiceCollection services)
        {
            var dbSettings = new CustomDbSettings();
            Configuration.Bind(_DBSETTING, dbSettings);
            //if (!string.IsNullOrEmpty(KeyEncripct)) dbSettings.ConnectionString = Core.SSB
            //        .Lib.Helpers.CryptoHelper.DecryptSHA256(dbSettings.ConnectionString, KeyEncripct);
            //Configuration.Bind(_DBSETTING, dbSettings);
            services.AddSingleton(dbSettings);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            _SetCorsPolicy(app);

            app.UseAuthorization();

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
            });
            
        }
        private void _ConfigOthers(IServiceCollection services)
        {
            DependencyInjectionProfile.RegisterProfile(services, Configuration);
        }
        private void _SetCorsPolicy(IApplicationBuilder app)
        {
            app.UseCors(_NAMECORSPOLICY);
        }
    }
}