using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glamz.Business.Service
{
    public static class DataSettingsManager
    {
        public static string _connectionstring = "";

        private static DataSettings _dataSettings;

        private static bool? _databaseIsInstalled;

        public static DataSettings LoadSettings(bool reloadSettings = false)
        {
            if (_connectionstring != null && !string.IsNullOrEmpty(_connectionstring))
            {
                _dataSettings = new DataSettings();
                _dataSettings.ConnectionString = _connectionstring;
                _dataSettings.DbProvider = DbProvider.MongoDB;
            }
            return _dataSettings;
        }
    }
}
