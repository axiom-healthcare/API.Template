﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        private readonly Context _context;

        public EntitiesController(Context context)
        {
            _context = context;
        }

        // GET: api/Entities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entity>>> GetEntities()
        {
            return await _context.Entities.ToListAsync();
        }

        // GET: api/Entities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entity>> GetEntity(int id)
        {
            var entity = await _context.Entities.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // PUT: api/Entities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntity(int id, Entity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
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

        // POST: api/Entities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Entity>> PostEntity(Entity entity)
        {
            _context.Entities.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntity", new { id = entity.Id }, entity);
        }

        // DELETE: api/Entities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Entity>> DeleteEntity(int id)
        {
            var entity = await _context.Entities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        private bool EntityExists(int id)
        {
            return _context.Entities.Any(e => e.Id == id);
        }
    }
}