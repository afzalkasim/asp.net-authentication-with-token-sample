
using Glamz.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glamz.Business.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();
            services.AddControllersWithViews(options =>
            {
                options.InputFormatters.Insert(0, services.GetJsonPatchInputFormatter());
            });

            services.AddSwagger();

            services.AddHttpContextAccessor();

            services.AddMemoryCache();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                var jsonOutFormatter = options.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();
                if (jsonOutFormatter != null)
                {
                    //removing text/json as it is not approved media type for working with JSON
                    if (jsonOutFormatter.SupportedMediaTypes.Contains("text/json"))
                        jsonOutFormatter.SupportedMediaTypes.Remove("text/json");
                }
                options.Filters.Add(new ProducesAttribute("application/json"));
            })
           .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .AddJsonOptions(optios => optios.JsonSerializerOptions.IgnoreNullValues = false);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(o => o.AddPolicy("GlamzPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddAppDependencies(Configuration);
            services.AddSecurity(Configuration);
            services.AddMapper();
            services.AddRouting(Option => Option.LowercaseUrls = true);
            services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            try
            {
                DataSettingsManager._connectionstring = Configuration.GetValue<string>("MongoDbSettings:ConnectionString");

                DbEdmInitialization.DbInitiate(app, Configuration);

                app.UseRouting();
                if (env.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Glamz API");
                        c.DefaultModelsExpandDepth(2);
                        c.DefaultModelExpandDepth(2);
                        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    });
                }
                else
                {
                    app.Run(async (context) =>
                    {
                        await context.Response.WriteAsync("Glamz service is running");
                    });
                }

                app.UseCors(x => x
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
