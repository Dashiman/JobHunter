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
    public class RegistrationServiceTests
    {
        ServiceCollection services = new ServiceCollection();
        ServiceProvider sp;
        Users user;
        public RegistrationServiceTests()
        {
            user = new Users {Username = "Testowiec", Authority = 1, Email = "damian@damian", Firstname = "Testowiec", Lastname = "Testowy" ,Password="123"};
            services.AddTransient<IRegistrationService, RegistrationService>();
            //services.AddDbContext<JobHunterContext>(options => options.UseInMemoryDatabase("jobhunter"));

            services.AddDbContext<JobHunterContext>(options => options.UseSqlServer("Server=localhost;Initial Catalog=jobhunter;Integrated Security=true;"));
                sp = services.AddLogging().BuildServiceProvider();
        }


        [Test]

        public async Task RegisterUserAsync_CompleteData_ReturnsTrue()
        {

            var j = sp.GetService<IRegistrationService>();
            var res = await j.RegisterUserAsync(user);

            Assert.IsTrue(res);
        }    
        [Test]

        public async Task RegisterUserAsync_EmptyObject_ReturnsArgumentNullException()
        {

            var j = sp.GetService<IRegistrationService>();
            var res = await j.RegisterUserAsync(new Users());

            Assert.Throws<ArgumentNullException>(() => { throw new ArgumentNullException(); }); ;
        }    
        [Test]

        public async Task RegisterUserAsync_NoPassword_ReturnsArgumentNullException()
        {

            var j = sp.GetService<IRegistrationService>();
            var us = user;
            us.Password = null;
            var res = await j.RegisterUserAsync(us);

            Assert.Throws<ArgumentNullException>(() => { throw new ArgumentNullException(); }); ;
        }
    }
}
