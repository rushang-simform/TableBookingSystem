using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Domain.Exceptions.Common
{
    public class CustomException : Exception
    {
        public CustomException()
        {

        }
        public CustomException(string message) : base(message)
        {

        }
    }

    public class MessageException : CustomException
    {
        public MessageException(string message) : base(message)
        {

        }
    }
}
