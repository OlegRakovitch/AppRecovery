using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.Providers
{
    interface IDataProvider
    {
        IEnumerable<T> SelectAll<T>();

        T SelectByFilter<T>(String filter);

        int Insert<T>(T entity);

        int UpdateById<T>(T entity);

        int DeleteById<T>(int id);
    }
}
