﻿using faceitapi.Context;
using faceitapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace faceitapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecuperarSenhaController : ControllerBase
    {
        private readonly faceitContext faceitContext;
        private readonly IConfiguration config;

        public RecuperarSenhaController(IConfiguration configuration, faceitContext context)
        {
            faceitContext = context;
            config = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterNovaSenha([FromBody] string email)
        {
            if (faceitContext.Pessoa.Any(x => x.Email.Equals(email)))
            {
                try
                {
                    var pessoa = await faceitContext.Pessoa.FirstOrDefaultAsync(x => x.Email.Equals(email));
                    Guid guid = Guid.NewGuid();

                    TrocarSenha(pessoa, guid.ToString());
                    SendEmail(pessoa, guid.ToString());

                    return Ok();
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private async void TrocarSenha(Pessoa pessoa, string senha)
        {
            try
            {
                pessoa.Senha = senha;
                faceitContext.Pessoa.Update(pessoa);
                await faceitContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SendEmail(Pessoa pessoa, string guid)
        {
            MailMessage mail = new MailMessage();

            StringBuilder body = new StringBuilder();
            body.AppendLine("Olá, você socilitou alteração de senha.");
            body.AppendLine($"Para acessar o aplicativo, use seu e-mail e a senha {guid}.");
            body.AppendLine("Após o login, poderá alterar sua senha pela edição de perfil.");

            mail.From = new MailAddress("faceitapisenaiarytorres@gmail.com");//de
            mail.To.Add(pessoa.Email); // para
            mail.Subject = "Recuperar senha"; // assunto
            mail.Body = body.ToString(); // mensagem

            // em caso de anexos
            //mail.Attachments.Add(new Attachment(@"C:\teste.txt"));

            using (var smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.EnableSsl = true; // GMail requer SSL
                smtp.Port = 587;       // porta para SSL
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                smtp.UseDefaultCredentials = false; // vamos utilizar credencias especificas

                // seu usuário e senha para autenticação
                smtp.Credentials = new NetworkCredential(config["Email:email"], config["Email:senha"]);

                // envia o e-mail
                smtp.Send(mail);
            }
        }

    }
}
