using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppRecoveryServer.Providers
{
    public abstract class SimpleDataProvider : AbstractDataProvider
    {
        private const String idColumn = "Id";

        protected override void PrepareSelectAll<T>(DbCommand command)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var tableName = type.Name;
            var columns = String.Join(",", properties.Select(property => property.Name));
            command.CommandText = $"select {columns} from {tableName}";
        }

        protected override void PrepareSelectByFilter<T>(DbCommand command, string filter)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var tableName = type.Name;
            var columns = String.Join(",", properties.Select(property => property.Name));
            command.CommandText = $"select {columns} from {tableName} where {filter}";
        }

        protected override T ParseSelectResult<T>(object[] values)
        {
            var type = typeof(T);
            var t = Activator.CreateInstance(type);
            var properties = type.GetProperties();
            int valueIterator = 0;
            foreach (var property in properties)
            {
                property.SetValue(t, Convert.ChangeType(values[valueIterator], property.PropertyType));
            }
            return (T)t;
        }

        protected override void PrepareInsert<T>(DbCommand command, T entity)
        {
            var type = typeof(T);
            var properties = type.GetProperties().Where(property => property.Name != idColumn);

            var tableName = type.Name;
            var columns = String.Join(",", properties.Select(property => property.Name));
            var values = String.Join(",", properties.Select(property => $"'{property.GetValue(entity)}'"));
            command.CommandText = $"insert into {tableName} ({columns}) values ({values})";
        }

        protected override void PrepareUpdateById<T>(DbCommand command, T entity)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var tableName = type.Name;
            var values = String.Join(",", properties.Where(property => property.Name != idColumn).Select(property => $"{property.Name} = '{property.GetValue(entity)}'"));
            var id = properties.Single(property => property.Name == idColumn).GetValue(entity);
            command.CommandText = $"update {tableName} set {values} where {idColumn} = {id}";
        }

        protected override void PrepareDeleteById<T>(DbCommand command, int id)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            command.CommandText = $"delete from {type.Name} where {idColumn} = {id}";
        }
    }
}
