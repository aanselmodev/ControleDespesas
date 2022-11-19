using ControleDespesas.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ControleDespesas.Libraries.Senha;

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

        public void EnviarNovaSenha(Usuario usuario, string url, string codigo)
        {
            string mensagem = $@" 
                <head>
                    <meta charset='UTF-8'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                </head>
                <body>
                    <header>
                    </header>
                    <main>
                        <div align='center'>
                            <h4>Seu código para redefinição de senha é:</h4>
                            <br />
                            <h2>{codigo}</h2>
                            <br />
                            <h4>Clique no link abaixo para cadastrar uma nova senha:</h4>    
                            <br />
                            <a href='{url}'>Gerar nova senha</a>
                            <br />
                            <br />
                            <p>Você será redirecionado para uma página da web.</p>
                            <p>E-mail enviado automaticamente por Controle de Despesas.</p>
                        </div>
                    </main>
                </body>";

            MailMessage msg = GerarMensagem(usuario.Email, "Controle de Despesas - Gerar Nova Senha", mensagem, true);

            _smtp.Send(msg);
        }


        public void EnviarConfirmacaoCadastro(Usuario usuario, string url)
        {
            string mensagem = $@" 
                <head>
                    <meta charset='UTF-8'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                </head>
                <body>
                    <header>
                    </header>
                    <main>
                        <div align='center'>
                            <h4>Clique no link abaixo para confirmar o seu cadastro:</h4>    
                            <br />
                            <a href='{url}'>Confirmar cadastro</a>
                            <br />
                            <br />
                            <p>Você será redirecionado para uma página da web.</p>
                            <p>E-mail enviado automaticamente por Controle de Despesas.</p>
                        </div>
                    </main>
                </body>";

            MailMessage msg = GerarMensagem(usuario.Email, "Controle de Despesas - Gerar Nova Senha", mensagem, true);

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
