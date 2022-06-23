using ApiTreino.Services.Interfaces;
using FluentResults;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Threading.Tasks;

namespace ApiTreino.Services.Implementation
{
    public class SvcEmail : ISvcEmail
    {
        private readonly IConfiguration _configuration;

        public SvcEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result> SendEmail(string email, string token, string userName)
        {
            var svcMessageActivationLink = new SvcMessageActivationLink(token, userName);
            var apikey = File.ReadAllText(@"C:\Users\DanielRodriguesCarva\source\repos\ApiTreino\sendgridkey.txt");
            var client = new SendGridClient(apikey);
            var from = new EmailAddress(_configuration.GetValue<string>("EmailSettings:From"), "TSSDANIEL");
            var assunto = "E-mail de confirmação TSS";
            var to = new EmailAddress(email, userName + "Email");
            var content = token;
            var htmlContent = svcMessageActivationLink.GetHtmlContent();
            var msg = MailHelper.CreateSingleEmail(from, to, assunto, content, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Result.Ok();
            }
            return Result.Fail("status code: " + response.StatusCode.ToString());
        }
    }
}
