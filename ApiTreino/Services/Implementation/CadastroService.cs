using ApiTreino.Dto;
using ApiTreino.Requests;
using ApiTreino.Services.Interfaces;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Linq;

namespace ApiTreino.Services.Implementation
{
    public class CadastroService : ICadastroService
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;
        private readonly ISvcEmail svcEmail;

        public CadastroService(UserManager<IdentityUser<int>> userManager, IMapper mapper, ISvcEmail svcEmail, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            this.svcEmail = svcEmail;
            _roleManager = roleManager;
        }

        public async Task<Result> CadastrarAsync(UserDto userDto)
        {
            var user = _mapper.Map<IdentityUser<int>>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Senha);

            if (result.Succeeded)
            {
                try
                {
                    if (!await _roleManager.RoleExistsAsync("regular"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<int>("regular"));
                    }
                    await _userManager.AddToRoleAsync(user, "regular");
                    var resultEmail = await GenerateEmailConfirmationToken(user);
                    if (resultEmail.IsSuccess)
                        return Result.Ok().WithSuccess("usuário criado! por favor verificar e-mail para confirmação.");
                }
                catch (Exception)
                {
                    await _userManager.DeleteAsync(user);
                    return Result.Fail("falha ao enviar e-mail de confirmação");
                };
            };

            return Result.Fail(result.Errors.First().Description);
        }

        public async Task<Result> AtivarContaAsync(RequestAtivacaoConta request)
        {
            var userName = request.UserName;
            var token = request.Token;
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return Result.Fail("usuario não encontrado");
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return Result.Ok();
            return Result.Fail("e-mail não confirmado");
        }

        public async Task<Result> GenerateEmailConfirmationToken(IdentityUser<int> user)
        {
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(confirmationToken);
            return await svcEmail.SendEmail(user.Email, encodedToken, user.UserName);
        }
    }
}
