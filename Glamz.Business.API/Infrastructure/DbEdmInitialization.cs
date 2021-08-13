using Glamz.Business.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glamz.Business.API
{
    public static class DbEdmInitialization
    {
        public static bool DbInitiate(this IApplicationBuilder app, IConfiguration configuration)
        {
            bool issuccess = false;
            try
            {
                string connectionString = string.Empty;
                connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");

                var mdb = new GlamzDBContext();
                mdb.DatabaseExist(connectionString);

                var scope = app.ApplicationServices.CreateScope();
                var installationService = scope.ServiceProvider.GetService<Glamz.Business.Service.IInstallationService>();
                installationService.InstallEntity(connectionString);
            }
            catch (Exception ex)
            {
                throw;
            }
            return issuccess;
        }
    }
}
