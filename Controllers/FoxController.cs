using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab9.Data;
using Lab9.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab9.Controllers
{
    [Route("api/fox")]
    [ApiController]
    public class FoxController : ControllerBase
    {
        private readonly IFoxesRepository _repo;

        public FoxController(IFoxesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Fox> Get()
        {
            return _repo.GetAll().OrderByDescending(x => x.Loves).ThenBy(x => x.Hates);
        }

        [HttpPost]
        [Authorize("IsAdminJwt")]
        public IActionResult Post([FromBody] Fox fox)
        {
            _repo.Add(fox);

            return CreatedAtAction(nameof(Get), new { id = fox.Id }, fox);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var fox = _repo.Get(id);
            if (fox == null)
                return NotFound();

            return Ok(fox);
        }

        [HttpPut("love/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Love(int id)
        {
            var fox = _repo.Get(id);

            if (fox == null)
                return NotFound();

            fox.Loves++;
            _repo.Update(id, fox);

            return Ok(fox);
        }

        [HttpPut("hate/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Hate(int id)
        {
            var fox = _repo.Get(id);

            if (fox == null)
                return NotFound();

            fox.Hates++;
            _repo.Update(id, fox);

            return Ok(fox);
        }
    }
}
