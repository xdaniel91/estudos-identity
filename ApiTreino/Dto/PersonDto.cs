using ApiTreino.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;

namespace ApiTreino.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Celular { get; set; }

    }
}
