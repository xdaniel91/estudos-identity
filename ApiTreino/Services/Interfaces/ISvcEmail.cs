using FluentResults;
using System.Threading.Tasks;

namespace ApiTreino.Services.Interfaces
{
    public interface ISvcEmail
    {
        public Task<Result> SendEmail(string email, string token, string userName);
    }
}
