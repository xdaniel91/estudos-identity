using ApiTreino.Dto;
using ApiTreino.Requests;
using ApiTreino.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTreino.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroService svcCadastro;

        public CadastroController(ICadastroService svcCadastro)
        {
            this.svcCadastro = svcCadastro;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var resultCadastro = await svcCadastro.CadastrarAsync(userDto);
            if (resultCadastro.IsSuccess)
                return Ok(resultCadastro.Successes[^1]);
            return BadRequest(resultCadastro.Errors[^1]);
        }

        [HttpGet("/ativacao")]
        public async Task<IActionResult> PutAsync([FromQuery] RequestAtivacaoConta request)
        {
            var resultAtivacao = await svcCadastro.AtivarContaAsync(request);
            if (resultAtivacao.IsSuccess)
                return Ok("conta ativada com sucesso!");
            return BadRequest(resultAtivacao.Errors[^1]);
        }
    }
}
