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
        public abstract void CreateTables();

        protected T ExecuteCommand<T>(Func<DbCommand, T> execute)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    return execute(command);
                }
            }
        }

        protected void ExecuteCommand(Action<DbCommand> execute)
        {
            ExecuteCommand(command =>
            {
                execute(command);
                return true;
            });
        }

        public IEnumerable<T> SelectAll<T>()
        {
            var propertiesCount = typeof(T).GetProperties().Length;

            return ExecuteCommand(command =>
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
            });
        }

        public T SelectByFilter<T>(string filter)
        {
            var propertiesCount = typeof(T).GetProperties().Length;

            return ExecuteCommand(command =>
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
            });
        }

        public int Insert<T>(T entity)
        {
            return ExecuteCommand(command =>
            {
                PrepareInsert<T>(command, entity);
                return command.ExecuteNonQuery();
            });
        }

        public int UpdateById<T>(T entity)
        {
            return ExecuteCommand(command =>
            {
                PrepareUpdateById<T>(command, entity);
                return command.ExecuteNonQuery();
            });
        }

        public int DeleteById<T>(int id)
        {
            return ExecuteCommand(command =>
            {
                PrepareDeleteById<T>(command, id);
                return command.ExecuteNonQuery();
            });
        }
    }
}
