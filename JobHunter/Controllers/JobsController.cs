using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using Services.VM;

namespace JobHunter.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/jobs")]
    [ApiController]
        public class JobsController : ControllerBase
    {

        protected readonly IJobService _job;
        public JobsController(IJobService job)
        {
            _job = job;
        }


        [HttpPut("[action]")]
        [Route("EditOffer")]
        public async Task<IActionResult> EditOffer([FromBody] Model.JobOffer offer)
        {
            
            var result = await _job.UpdateOffer(offer);
            return Ok(result);
        }  
        [HttpPut("[action]")]
        [Route("CloseCourse")]
        public async Task<IActionResult> EndCourse([FromBody]EndModel offer)
        {
            
            var result = await _job.EndCourse(offer);
            return Ok(result);
        }  
        [HttpPost("[action]")]
        [Route("AddOffer")]
        public async Task<IActionResult> AddNewOffer([FromBody]Model.JobOffer offer)
        {
            offer.AddedById = (int)HttpContext.Session.GetInt32("userid");
            offer.Status = 1;
            var result = await _job.Add(offer);
            return Ok(result);
        }   
        [HttpPost("[action]")]
        [Route("GiveJob")]
        public async Task<IActionResult> TakeJob([FromBody] Model.BidOffer offer)
        {

            var result = await _job.TakeJob(offer);
            return Ok(result);
        }
        [HttpPost("[action]")]
        [Route("ApplyFor")]
        public async Task<IActionResult> AddBid([FromBody] Model.BidOffer offer)
        {
            offer.UserId = HttpContext.Session.GetInt32("userid");
            var result = await _job.AddBid(offer);
            return Ok(result);
        }
        [HttpPost("[action]"), DisableRequestSizeLimit]
        [Route("Upload")]
        public async Task<IActionResult> AddManyOffers()
        {
            var file = Request.Form.Files[0];

            using (var reader = file.OpenReadStream())
            using (var csv = new CsvReader(new StreamReader(reader), CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<JobMap>();
                csv.Configuration.Delimiter = ";";
                csv.Configuration.Encoding = Encoding.Default; 
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.IncludePrivateMembers = true;
                var UserId = HttpContext.Session.GetInt32("userid");

                var records = csv.GetRecords<Model.JobOffer>();
                foreach(var t in records)
                {
                    t.AddedById =(int) UserId;
                    t.Status =1;
                     _job.Add(t);
                }
            }

            return Ok(1);
        } 
        [HttpDelete("[action]")]
        [Route("DeleteOffer/{offId:int}")]
        public async Task<IActionResult> DeleteOffer(int offId)
        {
            var result = await _job.DeleteOffer(offId);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetOffers")]
        public async Task<IActionResult> GetList()
        {
          
            var result = await _job.GetAll();
            return Ok(result);
        }  
        [HttpGet]
        [Route("GetOffer/{id:int}")]
        public async Task<IActionResult> GetOffer(int id)
        {
          
            var result = await _job.GetOffer(id);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetPD/{id:int}")]
        public async Task<IActionResult> GetProfileData(int id)
        {
          
            var result = await _job.GetProfileData(id);
            return Ok(result);
        }

    }
    public class JobMap : ClassMap<Model.JobOffer>
    {
        public JobMap()
        {
            Map(m => m.DeclaredCost).Index(2).TypeConverterOption.CultureInfo(new CultureInfo("pl-PL"));
            Map(m => m.Description).Index(1);
            Map(m => m.EndOfferDate).Index(3);
            Map(m => m.Title).Index(0);
            Map(m => m.CategoryId).Index(4);
        }
    }
}