using ApiTreino.Dto;
using ApiTreino.Repositories.Interfaces;
using ApiTreino.Services.Interfaces;
using ApiTreino.Validators;
using AutoMapper;
using FluentResults;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTreino.Services.Implementation
{
    public class SvcPerson : ISvcPerson
    {
        private readonly IRepositoryPerson _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Person> validator = new PersonValidator();

        public SvcPerson(IRepositoryPerson repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Add(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            var validationResult = validator.Validate(person);
            if (!validationResult.IsValid)
            {
                var erros = validationResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
                var errosString = string.Join(",", erros);
                throw new ArgumentException($"Informações inconsistentes.{Environment.NewLine}{errosString}");
            }
            await _repository.Add(person);
            return Result.Ok();
        }

        public Result Delete(PersonDto personDto)
        {
            _repository.Delete(_mapper.Map<Person>(personDto));
            return Result.Ok();
        }

        public async Task<Result> DeleteById(int id)
        {
            await _repository.DeleteById(id);
            return Result.Ok();
        }

        public async Task<List<PersonDto>> Get()
        {
            var persons = await _repository.Get();
           return _mapper.Map<List<PersonDto>>(persons);
        }

        public Result Update(PersonDto personDto)
        {
            _repository.Update(_mapper.Map<Person>(personDto));
            return Result.Ok();
        }
    }
}
