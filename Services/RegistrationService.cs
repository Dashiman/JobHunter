using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model;

namespace Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(Users user);
        Task<Users> Update(Users user);

    }

    public class RegistrationService : IRegistrationService
    {
        private readonly JobHunterContext _db;
        private readonly ILogger _logger;

        public RegistrationService(JobHunterContext db, ILogger<JobService> logger)
        {
            _logger = logger;

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
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<Users> Update(Users user)
        {
            var res = new Users();
            try
            {

                var Editeduser = _db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                if (Editeduser != null)
                {
                    _db.Entry(Editeduser).CurrentValues.SetValues(user);
                    _db.Users.Update(Editeduser);
                    await _db.SaveChangesAsync();
                    return Editeduser;
                }
                else
                {
                    res = user;
                    return res;

                }
            }

            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return res;
        }
    }
}