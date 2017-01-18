using AppRecoveryServer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AppRecoveryServer.Tests
{
    internal static class TestTools
    {
        public static String HashPassword(String password)
        {
            var md5 = MD5.Create();
            var bytes = GetBytes(password);
            var passwordHash = md5.ComputeHash(bytes);
            return GetString(passwordHash);
        }

        public static void ClearTable<T>()
        {
            var sqlite = new ExtendedSqliteDataProvider();
            sqlite.ClearTable<T>();
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes);
            return hex.Replace("-", "");
        }

        internal class ExtendedSqliteDataProvider : SqliteDataProvider
        {
            public void ClearTable<T>()
            {
                ExecuteCommand(command =>
                {
                    command.CommandText = $"delete from {typeof(T).Name}";
                    command.ExecuteNonQuery();
                });
            }
        }
    }
}
