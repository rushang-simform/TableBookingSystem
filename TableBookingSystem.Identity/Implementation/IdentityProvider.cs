using SmartTable.Application.DTOs.Response;
using SmartTable.Application.Intefaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartTable.Identity.Implementation
{
    public class IdentityProvider : IIdentityProvider
    {
        public AuthUserResponse GetAuthenticateUser()
        {
            throw new NotImplementedException();
        }
    }
}
