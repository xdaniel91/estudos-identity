using FluentResults;
using System.Threading.Tasks;

namespace ApiTreino.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public Task RollBack()
        {
            return Task.CompletedTask;
        }

        public async Task<Result> SaveChanges()
        {
            var sucess = await (_context.SaveChangesAsync()) > 1;
            if (sucess)
                return Result.Ok();
            return Result.Fail("persistence failed");
        }
    }

    public interface IUnitOfWork
    {
        public Task<Result> SaveChanges();
        public Task RollBack();
    }
}
