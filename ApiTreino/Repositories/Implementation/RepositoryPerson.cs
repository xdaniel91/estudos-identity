using ApiTreino.Repositories.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTreino.Repositories.Implementation
{
    public class RepositoryPerson : IRepositoryPerson
    {

        private readonly ApplicationContext _context;
        private readonly DbSet<Person> _persons;

        public RepositoryPerson(ApplicationContext context)
        {
            _context = context;
            _persons = context.persons;
        }

        public async Task<Result<Person>> Add(Person person)
        {
            await _persons.AddAsync(person);
            return Result.Ok(person);
        }

        public void Delete(Person person)
        {
            _persons.Remove(person);
        }

        public async Task DeleteById(int id)
        {
            var person = await _persons.FindAsync(id);
            if (person != null) _persons.Remove(person);
            throw new Exception("Person not found.");
        }

        public async Task<IEnumerable<Person>> Get()
        {
            return await _persons.ToListAsync();
        }

        public async Task<Person> GetById(int id)
        {
            return await _persons.FindAsync(id);
        }

        public void Update(Person person)
        {
            _persons.Update(person);
        }
    }
}
