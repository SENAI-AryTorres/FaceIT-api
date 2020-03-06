using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using faceitapi.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace faceitapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly faceitContext faceitContext;

        public SkillController()
        {
            faceitContext = new faceitContext();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            try
            {
                var data = await faceitContext.Skill
                .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}