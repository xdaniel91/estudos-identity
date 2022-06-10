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

        public async Task<bool> SaveChanges()
        {
            var sucess = await _context.SaveChangesAsync() > 0;
            return sucess;
        }
    }

    public interface IUnitOfWork
    {
        public Task<bool> SaveChanges();
        public Task RollBack();
    }
}
