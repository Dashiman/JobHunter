using NUnit.Framework;
using System.Security.Authentication.ExtendedProtection;
using Services;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using static Services.AuthService;
using System;

namespace JobHunterTests
{
    [TestFixture]
    public class AuthServiceTests
    {
        ServiceCollection serviceCollection;
        ServiceProvider serviceProvider;
        Users user;


        [SetUp]
        public void InitComponents()
        {
            user = new Users { Username = "test_user", Authority = 1, Email = "test_email@domain.com", Firstname = "Test", Lastname = "User", Password = "testuser" };
            serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IAuthService, AuthService>();
            serviceCollection.AddDbContext<JobHunterContext>(options => options.UseSqlServer("Server=tcp:jsdsprojects.database.windows.net,1433;Initial Catalog=TestProjectDatabase;Persist Security Info=False;User ID=jsarnowski;Password=SnPt_Gqx910S1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            serviceProvider = serviceCollection.AddLogging().BuildServiceProvider();
        }        

        [Test]
        public async Task Login_UserExists_ValidPassword_ReturnsSuccess()
        {
            var service = serviceProvider.GetService<IAuthService>();
            var result = await service.Login(this.user);
            Assert.That(result, Is.EqualTo((int)LoginStatuses.LogowanieSukces));
        }

        [Test]
        public async Task Login_UserExists_InvalidPassword_ReturnsFailure()
        {
            var service = serviceProvider.GetService<IAuthService>();
            var user = this.user;
            user.Password = "incorrect";
            var result = await service.Login(user);
            Assert.That(result, Is.EqualTo((int)LoginStatuses.BledneDaneLogowania));
        }

        [Test]
        public async Task Login_UserNotExists_ReturnsUserNotExists()
        {
            var service = serviceProvider.GetService<IAuthService>();
            var result = await service.Login(new Users());
            Assert.That(result, Is.EqualTo((int)LoginStatuses.BrakUzytkownika));
        }

        [Test]
        public async Task Login_UnknownError_ReturnsUnknownExceptionError()
        {
            var service = serviceProvider.GetService<IAuthService>();
            var result = await service.Login(new Users());
            Assert.Throws<Exception>(() => throw new Exception());
        }

        [Test]
        public async Task GetSessionData_UserExists_ReturnsUsersObjectReference()
        {
            var service = serviceProvider.GetService<IAuthService>();
            var result = await service.GetSessionData(user);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Users>(result);
        }

        [Test]
        public async Task GetSessionData_UserNotExists_ReturnsNullExceptionError()
        {
            var service = serviceProvider.GetService<IAuthService>();
            var result = await service.GetSessionData(new Users());
            Assert.That(result, Is.EqualTo(null));
            Assert.Throws<NullReferenceException>(() => throw new NullReferenceException());
        }
    }
}