using faceitapi.Context;
using faceitapi.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace faceitapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly faceitContext faceitContext;

        public LoginController()
        {
            faceitContext = new faceitContext();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginGet loginGet)
        {
            var pessoa = await faceitContext.Pessoa
                .FirstOrDefaultAsync(x => x.Email == loginGet.Email && (x.Senha == loginGet.Senha || x.GoogleId == loginGet.GoogleId));

            if (pessoa != null && pessoa.Excluido != true)
            {
                try
                {
                    if (pessoa.Tipo.Equals("PF"))
                    {
                        return Ok(
                            await faceitContext.PessoaFisica
                            .Include(x => x.IdpessoaNavigation)
                            .Include(x => x.IdpessoaNavigation.Endereco)
                            .Include(x => x.IdpessoaNavigation.PessoaSkill)
                            .Include(x => x.IdpessoaNavigation.Anexo)
                            .Include(x => x.IdpessoaNavigation.Imagem)
                            .FirstOrDefaultAsync(x => x.Idpessoa == pessoa.Idpessoa)
                            );
                    }
                    else
                    {
                        return Ok(
                            await faceitContext.PessoaJuridica
                            .Include(x => x.IdpessoaNavigation)
                            .Include(x => x.IdpessoaNavigation.Endereco)
                            .Include(x => x.IdpessoaNavigation.PessoaSkill)
                            .Include(x => x.IdpessoaNavigation.Anexo)
                            .Include(x => x.IdpessoaNavigation.Imagem)
                            .FirstOrDefaultAsync(x => x.Idpessoa == pessoa.Idpessoa)
                            );
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}