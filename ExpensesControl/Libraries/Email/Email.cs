using ExpensesControl.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ExpensesControl.Libraries;

namespace ExpensesControl.Libraries
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

        public void SendPasswordResetCode(User user, string url, string code)
        {
            string message = $@" 
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
                            <h4>seu código para redefinição de senha é:</h4>
                            <br />
                            <h2>{code}</h2>
                            <br />
                            <h4>clique no link abaixo para cadastrar uma nova senha:</h4>    
                            <br />
                            <a href='{url}'>gerar nova senha</a>
                            <br />
                            <br />
                            <p>você será redirecionado para uma página da web.</p>
                            <p>e-mail enviado automaticamente por Controle de Despesas.</p>
                        </div>
                    </main>
                </body>";

            MailMessage msg = CreateMessageBody(user.Email, "Controle de Despesas - Gerar Nova Senha", message, true);

            _smtp.Send(msg);
        }


        public void SendRegistrationConfirmation(User user, string url)
        {
            string message = $@" 
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
                            <h4>clique no link abaixo para confirmar o seu cadastro:</h4>    
                            <br />
                            <a href='{url}'>confirmar cadastro</a>
                            <br />
                            <br />
                            <p>você será redirecionado para uma página da web.</p>
                            <p>E-mail enviado automaticamente por Controle de Despesas.</p>
                        </div>
                    </main>
                </body>";

            MailMessage msg = CreateMessageBody(user.Email, "Controle de Despesas - Confirmação de Cadastro", message, true);

            _smtp.Send(msg);
        }

        public void SendUpdateEmailConfirmation(User user, string url)
        {
            string message = $@" 
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
                            <h4>clique no link abaixo para confirmar a alteração de e-mail:</h4>    
                            <br />
                            <a href='{url}'>confirmar e-mail</a>
                            <br />
                            <br />
                            <p>você será redirecionado para uma página da web.</p>
                            <p>E-mail enviado automaticamente por Controle de Despesas.</p>
                        </div>
                    </main>
                </body>";

            MailMessage msg = CreateMessageBody(user.Email, "Controle de Despesas - Confirmação de E-mail", message, true);

            _smtp.Send(msg);
        }

        private MailMessage CreateMessageBody(string recipientEmail, string subject, string message, bool isHtml)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(_config.GetValue<string>("Email:Username"));
            msg.To.Add(recipientEmail);
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = isHtml;

            return msg;
        }

    }
}
