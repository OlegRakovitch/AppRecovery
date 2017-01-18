using AppRecoveryServer.Models;
using AppRecoveryServer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.Managers
{
    public static class ItemsManager
    {
        private const String UserIdFilter = "UserId = '{0}'";
        private const String IdFilter = "UserId = '{0}' and Id = '{1}'";

        public static IEnumerable<Items> GetItems(int userId)
        {
            var dataProvider = DataProviderFactory.GetDataProvider();
            return dataProvider.SelectByFilter<Items>(String.Format(UserIdFilter, userId));
        }

        public static void InsertItem(int userId, String name, String description, int sort, String url)
        {
            var item = new Items();
            item.Name = name;
            item.Description = description;
            item.Sort = sort;
            item.Url = url;
            item.UserId = userId;

            var dataProvider = DataProviderFactory.GetDataProvider();
            dataProvider.Insert(item);
        }

        public static bool UpdateItem(int itemId, int userId, String name, String description, int sort, String url)
        {
            var dataProvider = DataProviderFactory.GetDataProvider();
            var items = dataProvider.SelectByFilter<Items>(String.Format(IdFilter, userId, itemId));
            if(!items.Any())
            {
                return false;
            }

            var item = items.Single();
            item.Name = name;
            item.Description = description;
            item.Sort = sort;
            item.Url = url;
            
            dataProvider.UpdateById(item);
            return true;
        }

        public static bool DeleteItem(int itemId, int userId)
        {
            var dataProvider = DataProviderFactory.GetDataProvider();
            var items = dataProvider.SelectByFilter<Items>(String.Format(IdFilter, userId, itemId));
            if (!items.Any())
            {
                return false;
            }

            dataProvider.DeleteById<Items>(itemId);
            return true;
        }
    }
}
