using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model;
using Moq;
using NUnit.Framework;
using Services;
using Services.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobHunterTests
{
    [TestFixture]
    public class JobServiceTests
    {

        ServiceCollection services = new ServiceCollection();
        ServiceProvider sp;
        Users user;
        JobOffer jobModel;
        BidOffer bid;
        public JobServiceTests()
        {
            //predefiniowany user z założeń aplikacji
            user = new Users { Id = 1, Username = "damian", Authority = 1, Email = "damian@damian", Firstname = "Damian", Lastname = "Szymański" };
            services.AddTransient<IJobService, JobService>();
            jobModel = new JobOffer { Title = "Tytuł Testowy", CategoryId = 1, Description = "Opis", EndOfferDate = DateTime.Now, AddedById = user.Id, Status = 1, DeclaredCost = 1522 };
            bid = new BidOffer { JobOfferId =7, OfferDate = DateTime.Now, Proposition = (decimal)15, UserId = user.Id };
           services.AddDbContext<JobHunterContext>(options => options.UseSqlServer("Server=localhost;Initial Catalog=jobhunter;Integrated Security=true;"));
            //services.AddDbContext<JobHunterContext>(options => options.UseInMemoryDatabase("jobhunter"));


            sp = services.AddLogging().BuildServiceProvider();
        }
        //add
        [Test]

        public async Task Add_CompleteData_ReturnsOne()
        {

            ////założenie: W bazie istnieje administrator o ID 1 (predefiniowany)
            var j = sp.GetService<IJobService>();
            var jm = jobModel;
            var res = await j.Add(jm);

            Assert.That(res,Is.EqualTo(1));
        } 
        [Test]

        public async Task Add_NullAddedBy_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
            var job = jobModel;
            job.AddedById = 0;
            var res = await j.Add(job);

            Assert.That(res,Is.EqualTo(0));
        }
        [Test]

        public async Task Add_EmptyObject_ReturnsZero()
        {
            //zgodnie z informacjami z sieci Inmemory databases nie są relacyjne, zatem pozwala to na nullowe obiekty
            var j = sp.GetService<IJobService>();
            var res = await j.Add(new JobOffer());

            Assert.That(res,Is.EqualTo(0));
        }   
        [Test]

        public async Task Add_NotExistingUser_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
            var job = jobModel;
            job.AddedById = 9999;
            var res = await j.Add(job); 


            Assert.That(res,Is.EqualTo(0));
        }  
        //addbid
        [Test]
        //metoda działa przy wywołaniu pojedynczym, przy wykonaniu wszystkich testów zwraca błąd
        //konstrukcja serwisu?
        public async Task AddBid_CompleteData_ReturnsOne()
        {
            var j = sp.GetService<IJobService>();
            var b = bid;
            var res = await j.AddBid(b); 


            Assert.That(res,Is.EqualTo(1));
        }     
        [Test]

        public async Task AddBid_NotExistingUser_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.AddBid(new BidOffer { JobOfferId=7,OfferDate=DateTime.Now,Proposition=(decimal)12.5,UserId=999}); 


            Assert.That(res,Is.EqualTo(0));
        }     
        [Test]

        public async Task AddBid_EmptyObject_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.AddBid(new BidOffer()); 


            Assert.That(res,Is.EqualTo(0));
        }  
        //TakeJob
        [Test]
        //metoda działa przy wywołaniu pojedynczym, przy wykonaniu wszystkich testów zwraca błąd
        public async Task TakeJob_HasJobId_ReturnsOne()
        {
            var j = sp.GetService<IJobService>();
            var jm = jobModel;

            await j.Add(jm);
            var res = await j.TakeJob(new BidOffer { JobOfferId = jm.Id, OfferDate = DateTime.Now, Proposition = 15, UserId = 1 }); 


            Assert.That(res,Is.EqualTo(1));
        }
        [Test]

        public async Task TakeJob_HasNotJobId_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
    

            var res = await j.TakeJob(new BidOffer {OfferDate = DateTime.Now, Proposition = 15, UserId = 1 }); 


            Assert.That(res,Is.EqualTo(0));
        }
        [Test]

        public async Task TakeJob_EmptyObject_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.TakeJob(new BidOffer()); 


            Assert.That(res,Is.EqualTo(0));
        }
        //getall

        [Test]

        public async Task GetAll_WhenCalled_ReturnsJobList()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.GetAll();
            Assert.IsNotNull(res);
            Assert.True(res.Count>=0);
            Assert.IsInstanceOf<List<JobOffer>>(res);
        }
        //GetProfileData - założenie istnieje użytkownik o ID  1 - administrator
        [Test]

        public async Task GetProfileData_WhenCalled_ReturnsJobList()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.GetProfileData(1);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<ProfileData>(res);
        }
        //getoffer
        [Test]

        public async Task GetOffer_WhenCalled_ReturnsJob()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.GetOffer(jobModel.Id);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<JobOffer>(res);
        }    [Test]

        public async Task GetOffer_WhenCalledButNotExist_ReturnsNull()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.GetOffer(9999);
            Assert.IsNull(res);
        }

        //deleteOffer
        [Test]

        public async Task DeleteOffer_WhenCalledButNotExxist_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.DeleteOffer(9999);
            Assert.That(res, Is.EqualTo(0));

        }  
        [Test]

        public async Task DeleteOffer_WhenCalledButExist_ReturnsOne()
        {
            var j = sp.GetService<IJobService>();
            var r1= new JobOffer { Title = "Tytuł Testowy", CategoryId = 1, Description = "Opis", EndOfferDate = DateTime.Now, AddedById = user.Id, Status = 1, DeclaredCost = 1522 };
            await j.Add(r1);
            
            var res = await j.DeleteOffer(r1.Id);
            Assert.That(res, Is.EqualTo(1));

        }
        //updateoffer
        [Test]

        public async Task UpdateOffer_WhenCalledButExist_ReturnsOne()
        {
            var j = sp.GetService<IJobService>();
            var jm = jobModel;
            await j.Add(jm);
            jm.Title = "edytowany testowy";
            jm.CategoryId = 2;
            jm.Description="testowany opis edycja";
            var res = await j.UpdateOffer(jm);
            Assert.That(res, Is.EqualTo(1));

        } 
        [Test]

        public async Task UpdateOffer_WhenCalledButNotExist_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();
            var res = await j.UpdateOffer(new JobOffer());
            Assert.That(res, Is.EqualTo(0));

        }

        //endcourse
        [Test]

        public async Task EndCourse_ValidData_ReturnsOne()
        {
            var j = sp.GetService<IJobService>();

            var jm = jobModel;
            await j.Add(jm);
            var em = new EndModel { OfferId = jm.Id, StatusId = jm.Status };
            var res = await j.EndCourse(em);
            Assert.That(res, Is.EqualTo(1));

        }
        [Test]

        public async Task EndCourse_NotExistingOffer_ReturnsZero()
        {
            var j = sp.GetService<IJobService>();

            var jm = jobModel;
            await j.Add(jm);
            var em = new EndModel { OfferId = 9897, StatusId = jm.Status };
            var res = await j.EndCourse(em);
            Assert.That(res, Is.EqualTo(0));

        }





    }
   
}
