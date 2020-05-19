using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

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
        public async Task<IActionResult> EditOffer([FromBody]JobOffer offer)
        {
            
            var result = await _job.UpdateOffer(offer);
            return Ok(result);
        }  
        [HttpPost("[action]")]
        [Route("AddOffer")]
        public async Task<IActionResult> AddNewOffer([FromBody]JobOffer offer)
        {
            offer.AddedById = (int)HttpContext.Session.GetInt32("userid");
            offer.Status = 1;
            var result = await _job.Add(offer);
            return Ok(result);
        }   
        [HttpPost("[action]")]
        [Route("GiveJob")]
        public async Task<IActionResult> TakeJob([FromBody]TakenOffer offer)
        {

            var result = await _job.TakeJob(offer);
            return Ok(result);
        }
        [HttpPost("[action]")]
        [Route("ApplyFor")]
        public async Task<IActionResult> AddBid([FromBody]BidOffer offer)
        {
            offer.UserId = HttpContext.Session.GetInt32("userid");
            var result = await _job.AddBid(offer);
            return Ok(result);
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

    }
}