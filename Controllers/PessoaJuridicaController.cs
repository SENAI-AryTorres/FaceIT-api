using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using faceitapi.Context;
using faceitapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await faceitContext.PessoaJuridica.Include(x => x.IdpessoaNavigation).ToListAsync();
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await faceitContext.PessoaJuridica.Include(x => x.IdpessoaNavigation).FirstOrDefaultAsync(x => x.Idpessoa == id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PessoaJuridica model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.IdpessoaNavigation.Excluido = false;
                    await faceitContext.Pessoa.AddAsync(model.IdpessoaNavigation);
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
        [HttpPut("{id}")]
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