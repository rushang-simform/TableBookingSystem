using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.ConfigurationClasses
{
    public class DBConfiguration
    {
        public string ServerHost { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string GetConnectionString()
        {
            return $"Server={ServerHost},{Port};user={Username};password={Password};database={Database}";
        }
    }
}
