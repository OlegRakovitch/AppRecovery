using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
    }
}
