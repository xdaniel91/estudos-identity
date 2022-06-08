using ApiTreino.Models;
using ApiTreino.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTreino
{
    public class Person : EntityBase
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Cpf Cpf { get; set; }
        public Email Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Celular { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age
        {
            get
            {
                var actualDate = DateTime.Now;
                var age = actualDate.Year - DateOfBirth.Year;

                if (actualDate.Month < DateOfBirth.Month)
                {
                    age--;
                }

                return age;
            }
        }

        public Person(string firstName, string lastName, Cpf cpf, Email email, DateTime date)
        {
            if (!cpf.EhValido) throw new ValidationException("Cpf is not valid");
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) throw new ValidationException("First name and last name is required");
            if (DateTime.UtcNow.Year - date.Year < 18 || DateTime.UtcNow.Year - date.Year > 110) throw new ValidationException("Age has to be between 18 and 110");
            if (!email.IsValid) throw new ValidationException("Email is not valid");
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            DateOfBirth = date;
            Cpf = cpf;
            Email = email;
        }
        public override string ToString() => FullName;
        protected Person() { }
    }
}
