using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.Shared.Configuration.Sms
{
    public class SendSmsConfiguration
    {
        public string BaseUrl { get; set; } 

        public string UserName { get; set; }
        public string Password { get; set; }
        public string PatternCode { get; set; }
        public string FromNo { get; set; }
    }
}
