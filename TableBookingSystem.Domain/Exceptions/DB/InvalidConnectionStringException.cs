using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Domain.Exceptions
{
    public class InvalidConnectionStringException : Exception
    {
        public InvalidConnectionStringException() : base()
        {

        }
    }
}
