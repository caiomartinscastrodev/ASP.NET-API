using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Models;
using API.IRepository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaiController : ControllerBase
    {
        private readonly IPaiRepository _paiRepository;
        private readonly AppDbContext _context;

        public PaiController(IPaiRepository paiRepository, AppDbContext context)
        {
            this._paiRepository = paiRepository;
            _context = context;
        }

        // GET: api/Pai
        [HttpGet]
        public ActionResult<IQueryable<Pai>> getAll()
        {
            return Ok(this._paiRepository.getAll());
        }

        // GET: api/Pai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pai>> GetPai(int id)
        {
            var pai = await this._paiRepository.get(id);

            if (pai == null)
            {
                return NotFound();
            }

            return pai;
        }

        // PUT: api/Pai/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPai(int id, Pai pai)
        {

            try
            {
                await this._paiRepository.put(id, pai);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pai
        [HttpPost]
        public async Task<ActionResult<Pai>> PostPai(Pai pai)
        {
            await this._paiRepository.post(pai);

            return CreatedAtAction("GetPai", new { id = pai.PaiId }, pai);
        }

        // DELETE: api/Pai/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePai(int id)
        {
            await this._paiRepository.delete(id);

            return NoContent();
        }

        private bool PaiExists(int id)
        {
            return this._context.Pai.Any(e => e.PaiId == id);
        }
    }
}
