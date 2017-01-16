using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.RequestBodies
{
    public class InsertItemRequest
    {
        public String caption { get; set; }
        public String description { get; set; }
        public int order { get; set; }
        public String url { get; set; }
    }

    public class UpdateItemRequest
    {
        public String caption { get; set; }
        public String description { get; set; }
        public int order { get; set; }
        public String url { get; set; }
    }
}
