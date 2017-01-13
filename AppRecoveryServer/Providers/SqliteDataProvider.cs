using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.Providers
{
    public class SqliteDataProvider : SimpleDataProvider
    {
        protected override DbConnection CreateConnection()
        {
            return new SqliteConnection(Configuration.ConnectionString);
        }
    }
}
