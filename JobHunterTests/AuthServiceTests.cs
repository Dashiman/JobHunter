using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Services;
using System;
using System.Threading.Tasks;
using static Services.AuthService;

namespace JobHunterTests
{
    public class AuthServiceTests
    {
        ServiceCollection services = new ServiceCollection();
        ServiceProvider sp;
        Users user;
        public AuthServiceTests()
        {
            //user istnieje w bazie, predefiniowany
            user = new Users { Username = "Testowiec", Authority = 1, Email = "damian@damian", Firstname = "Testowiec", Lastname = "Testowy", Password = "123" };
            services.AddTransient<IAuthService, AuthService>();


            services.AddDbContext<JobHunterContext>(options => options.UseSqlServer("Server=localhost;Initial Catalog=jobhunter;Integrated Security=true;"));
            //services.AddDbContext<JobHunterContext>(options => options.UseInMemoryDatabase("jobhunter"));

            sp = services.AddLogging().BuildServiceProvider();
        }
        [Test]
        public async Task Login_ExistingUser_ReturnsLogowanieSukces()
        {
            var service = sp.GetService<IAuthService>();
            var res = await service.Login(user);
            Assert.That(res,Is.EqualTo((int)LoginStatuses.LogowanieSukces));
        }   
        [Test]
        public async Task Login_ExistingUserInvalidData_ReturnsBledneDaneLogowania()
        {
            var service = sp.GetService<IAuthService>();
            var us = user;
            us.Password = "ee";
            var res = await service.Login(user);
            Assert.That(res,Is.EqualTo((int)LoginStatuses.BledneDaneLogowania));
        }
        [Test]
        public async Task Login_NotExistingUser_ReturnsBrakUzytkownika()
        {
            var service = sp.GetService<IAuthService>();

            var res = await service.Login(new Users());
            Assert.That(res,Is.EqualTo((int)LoginStatuses.BrakUzytkownika));
        }
        [Test]
        public async Task Login_UnknownError_ReturnsException()
        {
            var service = sp.GetService<IAuthService>();

            var res = await service.Login(new Users());
            Assert.Throws<Exception>(() => { throw new Exception(); });

        }
        [Test]
        public async Task GetSessionData_NotExistingUser_ReturnsNullException()
        {
            var service = sp.GetService<IAuthService>();

            var res = await service.GetSessionData(new Users());
            Assert.That(res, Is.EqualTo(null));

            Assert.Throws<NullReferenceException>(() => { throw new NullReferenceException(); }); 

        }
        [Test]
        public async Task GetSessionData_ExistingUser_ReturnsUser()
        {
            var service = sp.GetService<IAuthService>();

            var res = await service.GetSessionData(user);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<Users>(res);


        }
    }
}