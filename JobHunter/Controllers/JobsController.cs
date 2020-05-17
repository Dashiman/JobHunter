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
        [Route("GetOffers")]
        public async Task<IActionResult> GetList()
        {
          
            var result = await _job.GetAll();
            return Ok(result);
        }
    }
}