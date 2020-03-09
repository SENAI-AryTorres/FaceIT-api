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
    public class ImagemController : ControllerBase
    {
        private readonly faceitContext faceitContext;

        public ImagemController()
        {
            faceitContext = new faceitContext();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await faceitContext.Imagem
                    .ToListAsync();

                if (data.Count > 0)
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
        public async Task<IActionResult> Insert([FromBody] Imagem imagem)
        {
            try
            {
                await faceitContext.Imagem.AddAsync(imagem);
                await faceitContext.SaveChangesAsync();

                return Ok(imagem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}