using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Model;

namespace Services
{

    public interface IAuthService
    {
        Task<int> Login(Users user);
        Task<Users> GetSessionData(Users user);

    }



    public class AuthService : IAuthService
    {
        private readonly JobHunterContext _db;

        public AuthService(JobHunterContext db)
        {
            _db = db;
        }


        public async Task<int> Login(Users user)
        {
            try
            {
                Users userByLogin = await _db.Users.Where(a => a.Username == user.Username).FirstOrDefaultAsync();


                string[] parts = userByLogin.Password.Split(new char[] { ':' });

                byte[] saltBytes = Convert.FromBase64String(parts[2]);
                byte[] derived;

                int iterations = Convert.ToInt32(parts[0]);

                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    user.Password,
                    saltBytes,
                    iterations,
                    HashAlgorithmName.SHA512))
                {
                    derived = pbkdf2.GetBytes(64);
                }

                string new_hash = string.Format("{0}:{1}:{2}", 2048, Convert.ToBase64String(derived), Convert.ToBase64String(saltBytes));

                if (userByLogin.Password == new_hash)
                {


                    return (int)LoginStatuses.LogowanieSukces;


                }
                else
                    return (int)LoginStatuses.BledneDaneLogowania;

            }
            catch (NullReferenceException NEx)
            {

                return (int)LoginStatuses.BrakUzytkownika;
            }
            catch (Exception ex)
            {
                return (int)LoginStatuses.NieznanyKodBledu;
            }

        }
        public async Task<Users> GetSessionData(Users user)
        {
            try
            {
                user = await _db.Users.Where(u => u.Username == user.Username).Select(u => new Users
                {
                    Id = u.Id,
                    Username = u.Username,
                    Authority = u.Authority
                }).FirstOrDefaultAsync();
                return user;
            }
            catch (NullReferenceException ex)
            {
                return user;
            }
        }
        public enum LoginStatuses
        {
            LogowanieSukces = 1,
            BrakUzytkownika = 2,
            BledneDaneLogowania = 3,
            NieznanyKodBledu = 4

        }
        public enum UserAuthorities
        {
            Redaktor = 1,
            Admin = 2,
            Guest=3

        }
    }
}