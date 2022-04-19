using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TableBookingSystem.Application.ConfigurationClasses;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.User;
using TableBookingSystem.Application.Features.User.Queries;
using TableBookingSystem.Application.Intefaces.Cryptography;
using TableBookingSystem.Application.Intefaces.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IOptions<JWTConfig> _options;
        private readonly IMediator _mediator;
        private readonly ICryptographyService _cryptographyService;
        public AccountService(IOptions<JWTConfig> options, IMediator mediator, ICryptographyService cryptographyService)
        {
            _mediator = mediator;
            _cryptographyService = cryptographyService;
            _options = options;
        }
        public async Task<AuthenticationResponse> Authenticate(string emailId, string password)
        {
            var response = new AuthenticationResponse() { IsSuccess = false };
            var userResponse = await _mediator.Send<GenericResponse<AuthUserInfoDto>>(new GetAuthUserInfoQuery() { EmailId = emailId });

            if (!userResponse.IsSuccess)
            {
                response.Message = "Incorrect Email or Passoword";
            }
            else
            {
                string encryptedPassword = _cryptographyService.EncryptPasswordWithSalt(password, userResponse.Data.PasswordSalt);

                Dictionary<string, string> claims = new Dictionary<string, string>();

                if (userResponse.Data.ResturantId != null)
                {
                    claims.Add("RestaurantId", userResponse.Data.ResturantId.ToString());
                }

                if (userResponse.Data.PasswordHash.Equals(encryptedPassword))
                {
                    response.UserId = userResponse.Data.UserId.ToString();
                    response.AuthToken = CreateToken(userResponse.Data.UserId.ToString(), userResponse.Data.EmailId, claims);
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "Incorrect Email or Passoword";
                }
            }
            return response;
        }
        private string CreateToken(string userId, string emailId, Dictionary<string, string> additionalClaims = null)
        {
            if (string.IsNullOrEmpty(_options.Value.EncryptionKey))
            {
                throw new Exception("No signing key found");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("UserId is null");
            }
            if (string.IsNullOrEmpty(emailId))
            {
                throw new ArgumentNullException("EmailId is null");
            }

            var key = Encoding.ASCII.GetBytes(_options.Value.EncryptionKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserId", userId));
            claims.Add(new Claim("EmailId", emailId));

            if (additionalClaims != null)
            {
                var otherClaims = additionalClaims.Select(x =>
                {
                    return new Claim(x.Key, x.Value);
                });

                claims.AddRange(otherClaims);
            }

            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var timeOut = DateTime.Now.AddMinutes(_options.Value.ExpirationTimeout);

            JwtSecurityToken jwtSecurityToken =
                new JwtSecurityToken(claims: claims, expires: timeOut, signingCredentials: signingCredentials,
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience);

            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
