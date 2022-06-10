using ApiTreino.Dto;
using ApiTreino.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiTreino.Controllers
{
    public class CadastroController
    {
        private readonly ICadastroService svcCadastro;

        [HttpPost]
        public IActionResult Cadastrar([FromBody] UserDto userDto)
        {
            
        }
    }
}
