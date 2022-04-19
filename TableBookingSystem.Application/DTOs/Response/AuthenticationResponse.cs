using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.DTOs.Response
{
    public class AuthenticationResponse : BaseResponse
    {
        public string UserId { get; set; }
        public string AuthToken { get; set; }
    }
}
