using ApiTreino.Requests;
using ApiTreino.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTreino.Services.Implementation
{
    public class SvcLogin : ISvcLogin
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly ISvcToken svcToken;

        public SvcLogin(SignInManager<IdentityUser<int>> signInManager, UserManager<IdentityUser<int>> userManager, ISvcToken svcToken)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.svcToken = svcToken;
        }

        public async Task<Result> LoginAsync(LoginRequest request)
        {
            var login = await _signInManager.PasswordSignInAsync(request.Username, request.Senha, false, false);

            if (login.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.SingleOrDefault();
                var loginToken = svcToken.GerarToken(user, role);
                return Result.Ok().WithSuccess($"logado com sucesso! {loginToken}");
            }
            return Result.Fail("login falhou!");
        }

        public Task ConfirmPhoneNumber(IdentityUser<int> user)
        {
            
        }
    }
}
