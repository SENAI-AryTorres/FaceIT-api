﻿using faceitapi.Context;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var data = await faceitContext.PessoaFisica
                .Include(x => x.IdpessoaNavigation)
                .Include(x => x.IdpessoaNavigation.Anexo)
                .Include(x => x.IdpessoaNavigation.Imagem)
                .Include(x => x.IdpessoaNavigation.Endereco)
                .Where(x => x.IdpessoaNavigation.Excluido == false)
                .ToListAsync();

            return Ok(data);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await faceitContext.PessoaFisica
                .Include(x => x.IdpessoaNavigation)
                .Include(x => x.IdpessoaNavigation.PessoaSkill)
                .Include(x => x.IdpessoaNavigation.Endereco)
                .Include(x => x.IdpessoaNavigation.Anexo)
                .Include(x => x.IdpessoaNavigation.Imagem)
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
                ModelState.AddModelError(ex.Message, "Contate um administrador");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromBody] PessoaFisica model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.IdpessoaNavigation.Excluido = false;
                    model.IdpessoaNavigation.Tipo = "PF";
                    await faceitContext.Pessoa.AddAsync(model.IdpessoaNavigation);
                    await faceitContext.PessoaFisica.AddAsync(model);
                    await faceitContext.Endereco.AddAsync(model.IdpessoaNavigation.Endereco);
                    await faceitContext.PessoaSkill.AddRangeAsync(model.IdpessoaNavigation.PessoaSkill);
                    await faceitContext.Imagem.AddAsync(model.IdpessoaNavigation.Imagem);
                    await faceitContext.Anexo.AddAsync(model.IdpessoaNavigation.Anexo);

                    await faceitContext.SaveChangesAsync();

                    return Created("/api/PessoaFisica", model );
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Contate um administrador");
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditOrDelete([FromBody] PessoaFisica model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    faceitContext.Pessoa.Update(model.IdpessoaNavigation);
                    faceitContext.PessoaFisica.Update(model);
                    faceitContext.Endereco.Update(model.IdpessoaNavigation.Endereco);
                    faceitContext.PessoaSkill.UpdateRange(model.IdpessoaNavigation.PessoaSkill);
                    faceitContext.Imagem.Update(model.IdpessoaNavigation.Imagem);
                    faceitContext.Anexo.Update(model.IdpessoaNavigation.Anexo);

                    await faceitContext.SaveChangesAsync();

                    return Accepted(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Contate um administrador");
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