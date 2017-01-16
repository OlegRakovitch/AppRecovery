using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.Providers
{
    public interface IDataProvider
    {
        void CreateTables();

        IEnumerable<T> SelectAll<T>();

        IEnumerable<T> SelectByFilter<T>(String filter);

        int Insert<T>(T entity);

        int UpdateById<T>(T entity);

        int DeleteById<T>(int id);
    }
}
