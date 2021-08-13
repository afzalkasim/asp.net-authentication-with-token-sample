
using Glamz.Business.Repository;
using Glamz.Business.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glamz.Business.API
{
    public static class RegisterDependencies
    {
        public static void AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {


            string connectionString = string.Empty;
            connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");

            var mongourl = new MongoUrl(connectionString);
            var databaseName = mongourl.DatabaseName;
            services.AddScoped(c => new MongoClient(mongourl).GetDatabase(databaseName));


            //Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBusinessRepository, BusinessRepository>();

            //Service
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEncryptionService, EncryptionService>();

            services.AddTransient<IGlamzDBContext, GlamzDBContext>();
            services.AddTransient(typeof(IGlamzRepository<>), typeof(GlamzRepository<>));
            services.AddScoped<IInstallationService, InstallationService>();

            services.AddTransient<IBusinessService, BusinessService>();

            var mailSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var appSettings = mailSettingsSection.Get<JwtSettings>();
            services.AddSingleton(appSettings);
        }

        public static void AddSecurity(this IServiceCollection services, IConfiguration Configuration)
        {
            #region JWT Authentication

            var appSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<JwtSettings>();

            services.AddSingleton(appSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = System.TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {

                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            #endregion

            services.AddRouting(Option => Option.LowercaseUrls = true);
        }


        /// <summary>
        /// Open API Swagger for the API Documentation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Glamz API Documentation",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Email = "support@rubix.com" },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense() { Name = "Licensed To Rubix Technologies" }
                });

                // c.CustomSchemaIds(x => x.Name);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
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
                            new string[] {}

                    }
                });
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();
                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
            });
            return services;
        }

        //Auto Mapper
        public static void AddMapper(this IServiceCollection services)
        {
            #region AutoMapper

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
        }

        public static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter(this IServiceCollection services)
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }
    }
}
