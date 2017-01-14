using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.Providers
{
    public static class DataProviderFactory
    {
        public static IDataProvider GetDataProvider()
        {
#if DEBUG
            return new SqliteDataProvider();
#endif
        }
    }
}
