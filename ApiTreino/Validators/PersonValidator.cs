using FluentValidation;

namespace ApiTreino.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person.Cpf).NotNull();
            RuleFor(person => person.Email).NotNull();
            RuleFor(person => person.FirstName).Length(3, 50);
            RuleFor(person => person.LastName).Length(3, 50);
            RuleFor(person => person.Age).GreaterThan(18);
            RuleFor(person => person.Celular).NotEmpty().NotNull();
        }
    }
}
