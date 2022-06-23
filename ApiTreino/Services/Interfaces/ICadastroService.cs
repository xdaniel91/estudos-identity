using ApiTreino.Dto;
using ApiTreino.Requests;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ApiTreino.Services.Interfaces
{
    public interface ICadastroService
    {
        public Task<Result> CadastrarAsync(UserDto userDto);
        public Task<Result> AtivarContaAsync(RequestAtivacaoConta request);
        public Task<Result> GenerateEmailConfirmationToken(IdentityUser<int> user);
    }
}
