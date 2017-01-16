using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer.RequestBodies
{
    public class SignInRequest
    {
        public String clientId { get; set; }
        public String clientSecret { get; set; }
    }

    public class SignUpRequest
    {
        public String clientId { get; set; }
        public String clientSecret { get; set; }
        public String email { get; set; }
    }
}
