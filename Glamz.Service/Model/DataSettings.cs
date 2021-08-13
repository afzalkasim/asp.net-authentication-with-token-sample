using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glamz.Business.Service
{
    public partial class DataSettings
    {

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Database type
        /// </summary>
        public DbProvider DbProvider { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(ConnectionString);
        }
    }

    public enum DbProvider
    {
        MongoDB
    }
}
