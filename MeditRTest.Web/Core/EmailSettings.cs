using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MeditRTest.Web.Core
{
    public class EmailSettings
    {
        public EmailSettings(IConfiguration configuration)
        {
            ApiKey = configuration.GetSection("EmailSettings").GetSection("ApiKey").Value;
            FromEmail = configuration.GetSection("EmailSettings").GetSection("FromEmail").Value;
            FromName = configuration.GetSection("EmailSettings").GetSection("FromName").Value;
            IsLocal = bool.Parse(configuration.GetSection("EmailSettings").GetSection("IsLocal").Value);
        }

        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public bool IsLocal { get; set; }
    }
}
