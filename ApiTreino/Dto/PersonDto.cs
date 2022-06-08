using ApiTreino.ValueObjects;
using System;

namespace ApiTreino.Dto
{
    public class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Cpf Cpf { get; set; }
        public Email Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Celular { get; set; }

    }
}
