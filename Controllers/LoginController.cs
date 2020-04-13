using faceitapi.Context;
using faceitapi.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace faceitapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly faceitContext faceitContext;
        private readonly IConfiguration config;

        public LoginController(IConfiguration configuration)
        {
            faceitContext = new faceitContext();
            config = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginGet loginGet)
        {
            var pessoa = await faceitContext.Pessoa
                .FirstOrDefaultAsync(x => x.Email == loginGet.Email && (x.Senha == loginGet.Senha || x.GoogleID == loginGet.GoogleId));

            if (pessoa != null && pessoa.Excluido != true)
            {
                try
                {
                    if (pessoa.Tipo.Equals("PF"))
                    {
                        return Ok(
                            await faceitContext.PessoaFisica
                            .Include(x => x.IDPessoaNavigation)
                            .Include(x => x.IDPessoaNavigation.Endereco)
                            .Include(x => x.IDPessoaNavigation.PessoaSkill)
                            .Include(x => x.IDPessoaNavigation.Anexo)
                            .Include(x => x.IDPessoaNavigation.Imagem)
                            .FirstOrDefaultAsync(x => x.IDPessoa == pessoa.IDPessoa)
                            );
                    }
                    else
                    {
                        return Ok(
                            await faceitContext.PessoaJuridica
                            .Include(x => x.IDPessoaNavigation)
                            .Include(x => x.IDPessoaNavigation.Endereco)
                            .Include(x => x.IDPessoaNavigation.PessoaSkill)
                            .Include(x => x.IDPessoaNavigation.Anexo)
                            .Include(x => x.IDPessoaNavigation.Imagem)
                            .FirstOrDefaultAsync(x => x.IDPessoa == pessoa.IDPessoa)
                            );
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Contate um administrador");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private string GerarToken()
        {
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expiry,
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }


    }
}