using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using faceitapi.Context;
using faceitapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut]
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