using ApiTreino.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiTreino.Database.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            var CpfConverter = new ValueConverter<Cpf, string>(v => v.ToString(), v => Cpf.Parse(v));

            var EmailConverter = new ValueConverter<Email, string>(v => v.ToString(), v => Email.Parse(v));

            builder
                .Property(person => person.Cpf)
                .HasConversion(CpfConverter);

            builder
                 .Property(company => company.Email)
                 .HasConversion(EmailConverter);

            builder
                .HasAlternateKey(p => new { p.Cpf, p.Email });

        }
    }
}
