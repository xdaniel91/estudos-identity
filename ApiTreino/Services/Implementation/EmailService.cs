using ApiTreino.ValueObjects;
using FluentResults;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace ApiTreino.Services.Implementation
{
    public class EmailService
    {
        public Result SendEmail(string[] destinatarios, string assunto, int userId, string code)
        {
            var mensagem = new EmailMessage(destinatarios, assunto, userId, code);
            var mensagemEmail = CreateEmail(mensagem);
            var result = SendAsync(mensagemEmail);
            if (result.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("falha ao enviar e-mail");
        }

        private static MimeMessage CreateEmail(EmailMessage mensagem)
        {
            var mensagemEmail = new MimeMessage();
            var remetente = "danielcursoteste@gmail.com";
            mensagemEmail.From.Add(new MailboxAddress("Tss", remetente));
            mensagemEmail.To.AddRange(mensagem.Destinatario);
            mensagemEmail.Subject = mensagem.Assunto;
            mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };
            return mensagemEmail;
        }

        private static async Task SendAsync(MimeMessage mimeMessage)
        {
            using var client = new SmtpClient();
            try
            {
                var email = "danielcursoteste@gmail.com";
                var senha = "dotnet455";
                var smtpServer = "smtp.gmail.com";
                var port = 465;

                client.Connect(smtpServer, port, true);
                client.AuthenticationMechanisms.Remove("XOUATH2");
                client.Authenticate(email, senha);
                client.Send(mimeMessage);
                await client.SendAsync(mimeMessage);
            }
            catch (System.Exception) { throw; }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
