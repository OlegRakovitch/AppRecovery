using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.Managers
{
    public static class StatusMessages
    {
        public const String OK = "OK";
        public const String UserNotFound = "User with specified credentials not found";
        public const String UserAlreadyExists = "User with specified login already exists";
        public const String ItemNotFound = "Item with specified identifier not found";
    }
}
