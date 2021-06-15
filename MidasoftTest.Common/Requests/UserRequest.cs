using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.Common.Requests
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}
