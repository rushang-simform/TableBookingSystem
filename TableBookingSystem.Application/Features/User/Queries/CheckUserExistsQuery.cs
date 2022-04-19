using MediatR;
using TableBookingSystem.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.Features.User.Queries
{
    public class CheckUserExistsQuery : IRequest<bool>
    {
        public string EmailId { get; set; }
    }
    public class CheckUserNotExistsQueryHandler : IRequestHandler<CheckUserExistsQuery, bool>
    {
        private readonly IUserRepository _userRepository;

        public CheckUserNotExistsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        {
            var isExists = _userRepository.CheckUserExists(request.EmailId);
            return isExists;
        }
    }
}

