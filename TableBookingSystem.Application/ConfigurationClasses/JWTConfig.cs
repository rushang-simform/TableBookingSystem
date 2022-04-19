using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.ConfigurationClasses
{
    public class JWTConfig
    {
        public string EncryptionKey { get; set; }
        public int ExpirationTimeout { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
