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
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly faceitContext faceitContext;

        public PessoaJuridicaController()
        {
            faceitContext = new faceitContext();
        }

        //Metodo vai ver removido para produção
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                System.Collections.Generic.List<PessoaJuridica> data = await faceitContext.PessoaJuridica
                    .Include(x => x.IdpessoaNavigation)
                    .ThenInclude(x => x.Endereco)
                    .Where(x => x.IdpessoaNavigation.Excluido == false)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                PessoaJuridica data = await faceitContext.PessoaJuridica
                    .Include(x => x.IdpessoaNavigation)
                    .Where(x => x.IdpessoaNavigation.Excluido == false)
                    .FirstOrDefaultAsync(x => x.Idpessoa == id);

                if (data != null)
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] PessoaJuridica model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.IdpessoaNavigation.Excluido = false;
                    model.IdpessoaNavigation.Tipo = "PJ";
                    await faceitContext.Pessoa.AddAsync(model.IdpessoaNavigation);
                    await faceitContext.Endereco.AddAsync(model.IdpessoaNavigation.Endereco);
                    await faceitContext.PessoaJuridica.AddAsync(model);
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

        /// <summary>
        /// Metodo serve tanto para atualizar como para apagar, já que o registro não será apagado fisicamente.
        /// </summary>
        /// <param name="model">Objeto modificado</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditOrDelete([FromBody] PessoaJuridica model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    faceitContext.Pessoa.Update(model.IdpessoaNavigation);
                    faceitContext.PessoaJuridica.Update(model);
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
    }
}