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
    public class FilhoController : ControllerBase
    {
        private readonly IFilhoRepository _FilhoRepository;
        private readonly AppDbContext _context;

        public FilhoController(IFilhoRepository FilhoRepository, AppDbContext context)
        {
            this._FilhoRepository = FilhoRepository;
            _context = context;
        }

        // GET: api/Filho
        [HttpGet]
        public ActionResult<IQueryable<Filho>> getAll()
        {
            return Ok(this._FilhoRepository.getAll());
        }

        // GET: api/Filho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filho>> GetFilho(int id)
        {
            var Filho = await this._FilhoRepository.get(id);

            if (Filho == null)
            {
                return NotFound();
            }

            return Filho;
        }

        // PUT: api/Filho/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilho(int id, Filho Filho)
        {

            try
            {
                await this._FilhoRepository.put(id, Filho);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilhoExists(id))
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

        // POST: api/Filho
        [HttpPost]
        public async Task<ActionResult<Filho>> PostFilho(Filho Filho)
        {
            await this._FilhoRepository.post(Filho);

            return CreatedAtAction("GetFilho", new { id = Filho.FilhoId }, Filho);
        }

        // DELETE: api/Filho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilho(int id)
        {
            await this._FilhoRepository.delete(id);

            return NoContent();
        }

        private bool FilhoExists(int id)
        {
            return this._context.Filho.Any(e => e.FilhoId == id);
        }
    }
}
