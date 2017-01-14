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

        public override void CreateTables()
        {
            ExecuteCommand(command =>
            {
                command.CommandText = "create table if not exists Users (Id integer primary key, Login text, Email text, Password text)";
                command.ExecuteNonQuery();
            });

            ExecuteCommand(command =>
            {
                command.CommandText = "create table if not exists Items (Id integer primary key, Name text, Description text, Sort integer, Url text, UserId integer)";
                command.ExecuteNonQuery();
            });
        }
    }
}
