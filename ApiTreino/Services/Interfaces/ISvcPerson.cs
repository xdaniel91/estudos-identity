using ApiTreino.Dto;
using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTreino.Services.Interfaces
{
    public interface ISvcPerson
    {
        public Task<Result> Add(PersonDto personDto);
        public Result Delete(PersonDto personDto);
        public Result Update(PersonDto personDto);
        public Task<Result> DeleteById(int id);
        public Task<List<PersonDto>> Get();

    }
}
