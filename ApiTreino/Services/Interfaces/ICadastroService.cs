using ApiTreino.Dto;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ApiTreino.Services.Interfaces
{
    public interface ICadastroService
    {
        public Task<IdentityResult> CadastrarAsync(UserDto userDto);
    }
}
