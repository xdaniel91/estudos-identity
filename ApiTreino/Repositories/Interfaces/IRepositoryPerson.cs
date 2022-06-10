using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTreino.Repositories.Interfaces
{
    public interface IRepositoryPerson
    {
        public Task<Result<Person>> Add(Person person);
        public void Delete(Person person);
        public void Update(Person person);
        public Task DeleteById(int id);
        public Task<IEnumerable<Person>> Get();
        public Task<Person> GetById(int id);

    }
}
