using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Microsoft.AspNetCore.Mvc;
using Model;
using Microsoft.AspNetCore.Cors;

namespace Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        protected readonly IRegistrationService _registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        [HttpPost("[action]")]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody]Users user)
        {

            var result = await _registrationService.RegisterUserAsync(user);
            return Ok(1);
        }
    }
}