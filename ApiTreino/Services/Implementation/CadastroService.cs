using ApiTreino.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ApiTreino.Services.Implementation
{
    public class CadastroService
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IMapper _mapper;

        public CadastroService(UserManager<IdentityUser<int>> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CadastrarAsync(UserDto userDto)
        {
            var user = _mapper.Map<IdentityUser<int>>(userDto);
            var result = await _userManager.CreateAsync(user);
            return result;
        }
    }
}
