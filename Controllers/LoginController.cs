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
                            await faceitContext.Pessoa
                            .Include(x => x.PessoaFisica)
                            .Include(x => x.Endereco)
                            .Include(x => x.PessoaSkill)
                            .Include(x => x.Anexo)
                            .Include(x => x.Imagem)
                            .FirstOrDefaultAsync(x => x.IDPessoa == pessoa.IDPessoa)
                            );
                    }
                    else
                    {
                        return Ok(
                            await faceitContext.Pessoa
                            .Include(x => x.PessoaJuridica)
                            .Include(x => x.Endereco)
                            .Include(x => x.PessoaSkill)
                            .Include(x => x.Anexo)
                            .Include(x => x.Imagem)
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
    }
}