
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glamz.Business.Entity;
using Glamz.Business.Repository;

namespace Glamz.Business.Service
{
    public partial class InstallationService : IInstallationService
    {
        private readonly IGlamzRepository<User> _userRepository;
        private readonly IGlamzRepository<Glamz.Business.Entity.Business> _buinessRepository;

        public InstallationService()
        {

        }
        private readonly IServiceProvider _serviceProvider;
        public InstallationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var dataProviderSettings = DataSettingsManager.LoadSettings(reloadSettings: true);

            string dbconnectionstring = dataProviderSettings.ConnectionString;
            _userRepository = new GlamzRepository<User>(dbconnectionstring);
            _buinessRepository = new GlamzRepository<Glamz.Business.Entity.Business>(dbconnectionstring);
        }

        public virtual async Task InstallEntity(string connectionstring)
        {
            if (!await IsDbExist())
                await CreateTables(connectionstring);
        }

        private async Task CreateTables(string connectionstring)
        {
            try
            {
                var dbContext = new GlamzDBContext(connectionstring);
                var appdomains = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.Contains("Glamz.Business.Entity")).ToList();
                if (appdomains != null && appdomains.ToList().Count() > 0)
                {
                    var types = appdomains[0].DefinedTypes;
                    foreach (var item in types)
                    {
                        if (item.BaseType != null && item.IsClass && item.BaseType == typeof(BaseEntity))
                            await dbContext.CreateTable(item.Name, "en");
                    }
                    AddNewUser();
                    //await CreateIndexes(dbContext);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<bool> AddNewUser()
        {
            bool issuccess = false;
            try
            {
                User objapi = new User()
                {
                    UserId = 1,
                    UserName = "admin",
                    Email = "saravanan@rubixtek.com",
                    IsActive = true,
                    Password = "fNqv+p4H8LeUs8s60FG+EhyZYjdGbiqqWkGZAEZtzKc=",
                    CreatedOn = DateTime.Now,
                    CreatedBy = 1
                };
                if (_userRepository != null)
                {
                    User existuser = await _userRepository.FirstOrDefaultAsync(x => x.Email.ToLowerInvariant() == objapi.Email.ToLowerInvariant());
                    if (existuser == null)
                    {
                        _userRepository.Insert(objapi);
                        issuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return issuccess;
        }

        public async Task<bool> IsDbExist()
        {
            bool isexist = false;
            if (_userRepository != null)
            {
                List<User> existuser = await _userRepository.GetAllAsync();
                if (existuser != null && existuser.Count > 0)
                {
                    isexist = true;
                }
            }
            return isexist;
        }

        private async Task CreateIndexes(GlamzDBContext dbContext)
        {
            await dbContext.CreateIndex(_userRepository, OrderBuilder<User>.Create().Ascending(x => x.UserId), "UserId");
        }
    }
}
