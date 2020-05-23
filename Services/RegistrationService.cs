using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(Users user);

    }

    public class RegistrationService : IRegistrationService
    {
        private readonly JobHunterContext _db;

        public RegistrationService(JobHunterContext db)
        {
            _db = db;
        }
        public async Task<bool> RegisterUserAsync(Users user)
        {
            try
            {
                var newUser = user;
                //set password 
                var c = new RNGCryptoServiceProvider();
                byte[] salt = new byte[128];
                byte[] passResult;
                c.GetBytes(salt);
                // Convert.ToBase64String(salt);
                using (var hash = new Rfc2898DeriveBytes(
                    user.Password,
                    salt,
                    2048,
                    HashAlgorithmName.SHA512))
                {
                    passResult = hash.GetBytes(64);
                }
                user.Password = string.Format("{0}:{1}:{2}", 2048, Convert.ToBase64String(passResult), Convert.ToBase64String(salt));





                await _db.Users.AddAsync(newUser);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}