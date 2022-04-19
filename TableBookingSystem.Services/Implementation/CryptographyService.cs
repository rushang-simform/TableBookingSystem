using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.Intefaces.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Services.Implementation
{
    public class CryptographyService : ICryptographyService
    {
        private readonly ILogger<CryptographyService> _logger;

        public CryptographyService(ILogger<CryptographyService> logger)
        {
            _logger = logger;
        }
        public Tuple<string, string> EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password is null");
            }
            using (_logger.BeginScope("Password encryption process started"))
            {

                string salt = GetSalt(),
                       encrypatedPassword = EncryptPasswordWithSalt(password, salt);

                return Tuple.Create<string, string>(encrypatedPassword, salt);
            }

        }
        public string EncryptPasswordWithSalt(string password, string salt)
        {
            try
            {
                _logger.LogInformation("Password encryption started with salt.");

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

                    StringBuilder builder = new StringBuilder();

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    string encryptedPasswordString = builder.ToString();

                    _logger.LogInformation("Password encryption completed. ");

                    return encryptedPasswordString;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Password encryption error");
                throw;
            }
        }
        private string GetSalt()
        {
            try
            {
                var bytes = new byte[128 / 8];
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Password Salt Generation error");
                throw;
            }

        }
    }
}
