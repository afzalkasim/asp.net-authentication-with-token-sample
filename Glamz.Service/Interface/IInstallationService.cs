using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Glamz.Business.Service
{
    public partial interface IInstallationService
    {
        Task InstallEntity(string connectionstring);
    }
}
