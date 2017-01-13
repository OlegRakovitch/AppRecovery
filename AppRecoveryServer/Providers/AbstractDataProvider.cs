using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppRecoveryServer.Providers
{
    public abstract class AbstractDataProvider : IDataProvider
    {
        protected abstract DbConnection CreateConnection();
        protected abstract void PrepareDeleteById<T>(DbCommand command, int id);
        protected abstract void PrepareInsert<T>(DbCommand command, T entity);
        protected abstract void PrepareSelectAll<T>(DbCommand command);
        protected abstract void PrepareSelectByFilter<T>(DbCommand command, String filter);
        protected abstract void PrepareUpdateById<T>(DbCommand command, T entity);
        protected abstract T ParseSelectResult<T>(object[] values);


        protected DbCommand CreateCommand()
        {
            using (var connection = CreateConnection())
            {
                return connection.CreateCommand();
            }
        }

        public IEnumerable<T> SelectAll<T>()
        {
            var propertiesCount = typeof(T).GetProperties().Length;
            using (var command = CreateCommand())
            {
                PrepareSelectAll<T>(command);
                using (var dataReader = command.ExecuteReader())
                {
                    List<T> results = new List<T>();
                    while (dataReader.Read())
                    {
                        var propertiesValues = new object[propertiesCount];
                        dataReader.GetValues(propertiesValues);
                        results.Add(ParseSelectResult<T>(propertiesValues));
                    }
                    return results;
                }
            }
        }

        public T SelectByFilter<T>(string filter)
        {
            var propertiesCount = typeof(T).GetProperties().Length;
            using (var command = CreateCommand())
            {
                PrepareSelectByFilter<T>(command, filter);
                using (var dataReader = command.ExecuteReader())
                {
                    object[] propertiesValues = null;
                    while (dataReader.Read())
                    {
                        if (propertiesValues == null)
                        {
                            propertiesValues = new object[propertiesCount];
                        }
                        else
                        {
                            throw new Exception("Data returned more than once");
                        }
                        dataReader.GetValues(propertiesValues);
                    }
                    return ParseSelectResult<T>(propertiesValues);
                }
            }
        }

        public int Insert<T>(T entity)
        {
            using (var command = CreateCommand())
            {
                PrepareInsert<T>(command, entity);
                return command.ExecuteNonQuery();
            }
        }

        public int UpdateById<T>(T entity)
        {
            using (var command = CreateCommand())
            {
                PrepareUpdateById<T>(command, entity);
                return command.ExecuteNonQuery();
            }
        }

        public int DeleteById<T>(int id)
        {
            using (var command = CreateCommand())
            {
                PrepareDeleteById<T>(command, id);
                return command.ExecuteNonQuery();
            }
        }
    }
}
