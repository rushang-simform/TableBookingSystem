using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableBookingSystem.Application.DTOs.Response;

namespace TableBookingSystem.Application.Intefaces.Auth
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> Authenticate(string emailId, string password);
    }
}
