using Microsoft.AspNetCore.Identity;

namespace ApiTreino.Services.Interfaces
{
    public interface ISvcToken
    {
        public string GerarToken(IdentityUser<int> user, string role);
    }
}
