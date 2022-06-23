using ApiTreino.Requests;
using ApiTreino.Services.Implementation;
using ApiTreino.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTreino.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ISvcLogin svcLogin;

        public LoginController(ISvcLogin svcLogin)
        {
            this.svcLogin = svcLogin;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LoginRequest request)
        {
            var result = await svcLogin.LoginAsync(request);
            if (result.IsSuccess)
                return Ok(result.Successes[^1]);
            return BadRequest(result.Errors[^1]);
        }
    }
}
