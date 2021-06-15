using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.Common.Responses
{
    public class UserTokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
