using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobHunterTests
{
    [TestFixture]
    class RegistrationServiceTests
    {
        ServiceCollection serviceCollection;
        ServiceProvider serviceProvider;
        Users user;

        [SetUp]
        public void InitComponents()
        {
            serviceCollection = new ServiceCollection();
            user = new Users { Username = "test_user", Authority = 1, Email = "test_email@domain.com", Firstname = "Test", Lastname = "User", Password = "testuser" };
            serviceCollection.AddTransient<IRegistrationService, RegistrationService>();
            serviceCollection.AddDbContext<JobHunterContext>(options => options.UseSqlServer("Server=tcp:jsdsprojects.database.windows.net,1433;Initial Catalog=TestProjectDatabase;Persist Security Info=False;User ID=jsarnowski;Password=SnPt_Gqx910S1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            serviceProvider = serviceCollection.AddLogging().BuildServiceProvider();
        }

        [Test]
        public async Task RegisterUserAsync_ValidForm_ReturnsTrue()
        {
            var service = serviceProvider.GetService<IRegistrationService>();
            var result = await service.RegisterUserAsync(user);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task RegisterUserAsync_EmptyForm_ReturnsFalse()
        {
            var service = serviceProvider.GetService<IRegistrationService>();
            var result = await service.RegisterUserAsync(new Users());
            Assert.IsFalse(result);
        }

    }
}
