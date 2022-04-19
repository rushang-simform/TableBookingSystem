using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.Intefaces.Cryptography
{
    public interface ICryptographyService
    {
        Tuple<string, string> EncryptPassword(string password);
        string EncryptPasswordWithSalt(string password, string salt);
    }
}
