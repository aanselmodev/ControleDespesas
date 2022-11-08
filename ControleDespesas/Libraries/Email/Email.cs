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

        public void EnviarNovaSenha(string email)
        {
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

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(_config.GetValue<string>("Email:Username"));
            msg.To.Add(usuario.Email);
            msg.Subject = "Controle de Despesas - Confirmação de Cadastro";
            msg.Body = mensagem;
            msg.IsBodyHtml = true;

            _smtp.Send(msg);
        }

    }
}
