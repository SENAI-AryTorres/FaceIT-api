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
    public class PropostaController : ControllerBase
    {
        private readonly faceitContext faceitContext;

        public PropostaController(faceitContext context)
        {
            faceitContext = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await faceitContext
                    .Proposta
                    .Include(x => x.PropostaSkill)
                    .Where(x => x.Encerrada == false)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Contate um administrador");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{idPropsota}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int idProposta)
        {
            try
            {
                var data = await faceitContext
                    .Proposta
                    .Include(x => x.PropostaSkill)
                    .Where(x => x.Encerrada == false && x.IDProposta == idProposta)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Contate um administrador");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromBody] Proposta proposta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    proposta.Encerrada = false;
                    await faceitContext.Proposta.AddAsync(proposta);
                    await faceitContext.PropostaSkill.AddRangeAsync(proposta.PropostaSkill);
                    await faceitContext.SaveChangesAsync();

                    return Created("/api/PessoaJuridica", proposta );
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
        /// Este metodo pode ser usado para editar ou excluir, tendo em vista que os dados nunca serão excluidos permanentemente.
        /// </summary>
        /// <param name="proposta"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditOrDelete([FromBody] Proposta proposta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    faceitContext.Proposta.Update(proposta);
                    faceitContext.PropostaSkill.UpdateRange(proposta.PropostaSkill);
                    await faceitContext.SaveChangesAsync();

                    return Accepted(proposta);
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