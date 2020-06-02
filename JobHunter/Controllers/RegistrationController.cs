using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Microsoft.AspNetCore.Mvc;
using Model;
using Microsoft.AspNetCore.Cors;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly JobHunterContext _db;

        protected readonly IRegistrationService _registrationService;
        public RegistrationController(IRegistrationService registrationService, JobHunterContext db)
        {
            _registrationService = registrationService;
            _db = db;

        }
        [HttpPost("[action]")]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody]Users user)
        {

            var result = await _registrationService.RegisterUserAsync(user);
            return Ok(1);
        }
        [HttpPut("[action]")]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateProfile([FromBody] Users user)
        {

            var result = await _registrationService.RegisterUserAsync(user);
            return Ok(result);
        }
        //TODO
        [HttpPost("[action]"), DisableRequestSizeLimit]
        [Route("Avatar")]
        public async Task<IActionResult> AddAvatar()
        {
            var file = Request.Form.Files[0];


            using (var memoryStream = new MemoryStream())
            {
                using var image = Image.Load(file.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                //image.Save("...");
            }

            return Ok(1);
        }
    }
}