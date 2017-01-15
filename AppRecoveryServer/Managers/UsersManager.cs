using AppRecoveryServer.Models;
using AppRecoveryServer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AppRecoveryServer.Managers
{
    public class UsersManager
    {
        public static int ValidateUser(String clientId, String clientSecret)
        {
            var dataProvider = DataProviderFactory.GetDataProvider();
            var users = dataProvider.SelectByFilter<Users>($"Login = '{clientId}' and Password = '{HashPassword(clientSecret)}'");
            if (!users.Any())
            {
                return 0;
            }
            else
            {
                return users.Single().Id;
            }
        }

        public static bool ValidateUserNotExists(String clientId)
        {
            var dataProvider = DataProviderFactory.GetDataProvider();
            var users = dataProvider.SelectByFilter<Users>($"Login = '{clientId}'");
            return !users.Any();
        }

        public static void CreateUser(String clientId, String clientSecret, String email)
        {
            var dataProvider = DataProviderFactory.GetDataProvider();
            var user = new Users();
            user.Email = email;
            user.Login = clientId;
            user.Password = HashPassword(clientSecret);
            dataProvider.Insert(user);
        }

        private static String HashPassword(String password)
        {
            var md5 = MD5.Create();
            var bytes = GetBytes(password);
            var passwordHash = md5.ComputeHash(bytes);
            return GetString(passwordHash);
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
    }
}
