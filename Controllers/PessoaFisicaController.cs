using faceitapi.Context;
using faceitapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace faceitapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaFisicaController : ControllerBase
    {
        private readonly faceitContext faceitContext;

        public PessoaFisicaController()
        {
            faceitContext = new faceitContext();
        }

        //Esse metodo será removido para produção
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            System.Collections.Generic.List<PessoaFisica> data = await faceitContext.PessoaFisica
                .Include(x => x.IdpessoaNavigation)
                .ThenInclude(x => x.Endereco)
                .Where(x => x.IdpessoaNavigation.Excluido == false)
                .ToListAsync();

            return Ok(data);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                PessoaFisica data = await faceitContext.PessoaFisica
                .Include(x => x.IdpessoaNavigation)
                .FirstOrDefaultAsync(x => x.Idpessoa == id);

                if (data.IdpessoaNavigation.Excluido == false)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditOrDelete([FromBody] PessoaFisica model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    faceitContext.Pessoa.Update(model.IdpessoaNavigation);
                    faceitContext.PessoaFisica.Update(model);
                    await faceitContext.SaveChangesAsync();
                    return Ok(GetById(model.Idpessoa));
                }
                catch (Exception)
                {

                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Metodo serve tanto para atualizar como para apagar, já que o registro não será apagado fisicamente.
        /// </summary>
        /// <param name="model">Objeto modificado</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] PessoaFisica model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.IdpessoaNavigation.Excluido = false;
                    model.IdpessoaNavigation.Tipo = "PF";
                    await faceitContext.Pessoa.AddAsync(model.IdpessoaNavigation);
                    await faceitContext.Endereco.AddAsync(model.IdpessoaNavigation.Endereco);
                    await faceitContext.PessoaFisica.AddAsync(model);
                    await faceitContext.SaveChangesAsync();

                    return Ok(GetById(model.Idpessoa));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}