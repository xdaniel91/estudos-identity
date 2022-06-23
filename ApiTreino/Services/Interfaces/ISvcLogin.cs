using ApiTreino.Requests;
using FluentResults;
using System.Threading.Tasks;

namespace ApiTreino.Services.Interfaces
{
    public interface ISvcLogin
    {
        public Task<Result> LoginAsync(LoginRequest request);
    }
}
