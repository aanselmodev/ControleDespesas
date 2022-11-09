using ControleDespesas.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ControleDespesas.Libraries.Email
{
    public class Email
    {
        private SmtpClient _smtp;
        private IConfiguration _config;

        public Email(SmtpClient smtp, IConfiguration config)
        {
            _smtp = smtp;
            _config = config;
        }

        public void EnviarNovaSenha(Usuario usuario)
        {
            string mensagem = $@"
            <h2>Sua nova senha foi gerada:</h2>    
            <h1>{usuario.Senha}</h2>
            <br />
            <h3>Você pode alterá-la utilizando o painel de controle.</h3>
            <br />
            <p>E-mail enviado automaticamente por Controle de Despesas.</p>
            ";

            MailMessage msg = GerarMensagem(usuario.Email, "Controle de Despesas - Redefinição de Senha", mensagem, true);

            _smtp.Send(msg);
        }


        public void EnviarConfirmacaoCadastro(Usuario usuario)
        {
            string mensagem = $@"
            <h2>Cadastro realizado com sucesso!</h2>    
            <h4>Parabéns, {usuario.Nome}, agora você pode organizar sua vida financeira!</h4>
            <br />
            <br />
            <p>E-mail enviado automaticamente por Controle de Despesas.</p>
            ";

            MailMessage msg = GerarMensagem(usuario.Email, "Controle de Despesas = Confirmação de Cadastro", mensagem, true);

            _smtp.Send(msg);
        }

        private MailMessage GerarMensagem(string emailDestinatario, string assunto, string mensagem, bool html)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(_config.GetValue<string>("Email:Username"));
            msg.To.Add(emailDestinatario);
            msg.Subject = assunto;
            msg.Body = mensagem;
            msg.IsBodyHtml = html;

            return msg;
        }

    }
}
