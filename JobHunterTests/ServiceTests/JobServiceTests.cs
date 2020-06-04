using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Services;
using Services.VM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobHunterTests
{
    [TestFixture]
    class JobServiceTests
    {
        ServiceCollection serviceCollection;
        ServiceProvider serviceProvider;
        Users user;
        JobOffer jobOffer;
        BidOffer bidOffer;

        [SetUp]
        public void InitComponents()
        {
            serviceCollection = new ServiceCollection();
            user = new Users { Username = "test_user", Authority = 1, Email = "test_email@domain.com", Firstname = "Test", Lastname = "User", Password = "testuser" };
            jobOffer = new JobOffer { Title = "Test offer", CategoryId = 1, Description = "Test offer description", EndOfferDate = DateTime.Now, AddedById = user.Id, Status = 1, DeclaredCost = 1000 };
            bidOffer = new BidOffer { JobOfferId = jobOffer.Id, OfferDate = DateTime.Now, Proposition = 1000, UserId = user.Id };
            serviceCollection.AddTransient<IJobService, JobService>();
            serviceCollection.AddDbContext<JobHunterContext>(options => options.UseSqlServer("Server=tcp:jsdsprojects.database.windows.net,1433;Initial Catalog=TestProjectDatabase;Persist Security Info=False;User ID=jsarnowski;Password=SnPt_Gqx910S1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            serviceProvider = serviceCollection.AddLogging().BuildServiceProvider();
        }

        [Test]
        public async Task Add_ValidForm_ReturnsOne()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            var result = await service.Add(jobOffer);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task Add_IncorrectAddedById_ReturnsZero()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            jobOffer.AddedById = 1000;
            var result = await service.Add(jobOffer);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task Add_EmptyJobOffer_ReturnsExceptionError()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.Add(new JobOffer());
            Assert.Throws<Exception>(() => throw new Exception());
        }

        [Test]
        public async Task AddBid_ValidBid_ReturnsOne()
        {
            var service = serviceProvider.GetService<IJobService>();
            var bidOffer = this.bidOffer;
            var result = await service.AddBid(bidOffer);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task AddBid_InvalidBid_ReturnsExceptionError()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.AddBid(new BidOffer());
            Assert.Throws<Exception>(() => throw new Exception());
        }

        [Test]
        public async Task AddBid_UserIdNotExists_ReturnsZero()
        {
            var service = serviceProvider.GetService<IJobService>();
            var bidOffer = this.bidOffer;
            bidOffer.UserId = 1000;
            var result = await service.AddBid(bidOffer);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task TakeJob_IsValid_ReturnsOne()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            var bidOffer = this.bidOffer;
            await service.Add(jobOffer);
            var result = await service.TakeJob(bidOffer);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task TakeJob_IsInvalid_ReturnsExceptionError()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            var result = await service.TakeJob(new BidOffer());
            Assert.Throws<Exception>(() => throw new Exception());
        }

        [Test]
        public async Task TakeJob_JobIdIsEmpty_ReturnsZero()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            jobOffer.Id = 0;
            var result = await service.TakeJob(new BidOffer());
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAll_WhenCalled_ReturnsJobList()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            var result = await service.GetAll();
            Assert.IsNotNull(result);
            Assert.True(result.Count >= 0);
            Assert.IsInstanceOf<List<JobOffer>>(result);
        }

        [Test]
        public async Task GetAll_JobListEmpty_ReturnsExceptionError()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.GetAll();
            Assert.Throws<Exception>(() => throw new Exception());
        }

        [Test]
        public async Task GetProfileData_WhenCalled_ReturnProfileDataObject()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.GetProfileData(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ProfileData>(result);
        }

        [Test]
        public async Task GetProfileData_ProfileIsEmpty_ReturnsExceptionError()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.GetProfileData(0);
            Assert.Throws<Exception>(() => throw new Exception());
        }

        [Test]
        public async Task GetOffer_WhenCalled_ReturnsOfferObject()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            var result = await service.GetOffer(jobOffer.Id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JobOffer>(result);
        }

        [Test]
        public async Task GetOffer_OfferIsEmpty_ReturnsExceptionError()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.GetOffer(1111);
            Assert.IsNull(result);
            Assert.Throws<Exception>(() => throw new Exception());
        }

        [Test]
        public async Task DeleteOffer_WhenCalled_ReturnsOne()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            await service.Add(jobOffer);
            var result = service.DeleteOffer(jobOffer.Id);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteOffer_OfferNotExists_ReturnsExceptionError()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.DeleteOffer(1111);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateOffer_WhenCalled_ReturnsOne()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            await service.Add(jobOffer);
            jobOffer.Title = "Updated job title";
            jobOffer.Description = "Updated job description";
            var result = await service.UpdateOffer(jobOffer);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateOffer_OfferNotExists_ReturnsZero()
        {
            var service = serviceProvider.GetService<IJobService>();
            var result = await service.UpdateOffer(new JobOffer());
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task EndCourse_WhenCalled_ReturnsOne()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            await service.Add(jobOffer);
            var endJobOfferModel = new EndModel { OfferId = jobOffer.Id, StatusId = jobOffer.Status };
            var result = await service.EndCourse(endJobOfferModel);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task EndCourse_OfferIsEmpty_ReturnsZero()
        {
            var service = serviceProvider.GetService<IJobService>();
            var jobOffer = this.jobOffer;
            await service.Add(jobOffer);
            var endJobOfferModel = new EndModel { OfferId = 1111, StatusId = jobOffer.Status };
            var result = await service.EndCourse(endJobOfferModel);
            Assert.That(result, Is.EqualTo(0));
        }

        [TearDown]
        public void Dispose()
        {
            user = null;
            jobOffer = null;
            bidOffer = null;
            serviceCollection = null;
            serviceProvider = null;
        }
    }
}
