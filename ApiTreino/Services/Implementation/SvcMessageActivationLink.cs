using ApiTreino.Services.Interfaces;

namespace ApiTreino.Services.Implementation
{
    public class SvcMessageActivationLink : ISvcMessageActivationLink
    {
        public string Content { get; set; }
        private string _formatedMessage;

        public SvcMessageActivationLink(string token, string userName)
        {
            var link = $"https://localhost:44387/ativacao?UserName={userName}&Token={token}";
            _formatedMessage = $"<strong> Olá {userName}, clique no link a seguir para verificar seu e-mail de cadastro no Tse.</strong>\n" + link;
        }

        public string GetHtmlContent()
        {
            return _formatedMessage;
        }
    }
}

