using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace ApiTreino.ValueObjects
{
    public class EmailMessage
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public EmailMessage(IEnumerable<string> destinatarios, string assunto, int userId, string codigoAtivacao)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatarios.Select(d => new MailboxAddress(d, d)));
            Assunto = assunto;
            Conteudo = @$"Olá, segue o link de ativação da sua conta.

            https://localhost:44303/ativacao?UsuarioId={userId}&CodigoAtivacao={codigoAtivacao}";
        }
    }
}
