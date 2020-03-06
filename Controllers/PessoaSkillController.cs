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
    public class PessoaSkillController : ControllerBase
    {
        private readonly faceitContext faceitContext;

        public PessoaSkillController()
        {
            faceitContext = new faceitContext();
        }

        [HttpGet("{idPessoa}")]
        public async Task<IActionResult> GetSkillsPessoa(int idPessoa)
        {
            try
            {
                var data = await faceitContext.PessoaSkill
                    .Where(x => x.Idpessoa == idPessoa)
                    .Include(x => x.Id)
                    .ToListAsync();

                if (data.Count > 0)
                {
                    return Ok(data);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] List<PessoaSkill> skills)
        {
            try
            {
                await faceitContext.PessoaSkill.AddRangeAsync(skills);
                await faceitContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSkillPessoa([FromBody] List<PessoaSkill> skills)
        {
            try
            {
                faceitContext.PessoaSkill.RemoveRange(skills);
                await faceitContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}