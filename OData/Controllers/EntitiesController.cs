using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace OData.Controllers
{
    public class EntitiesController: ODataController
    {
        private Context _context;

        public EntitiesController(Context context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Entities);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_context.Entities.FirstOrDefault(entity => entity.Id == key));
        }

        [EnableQuery]
        public IActionResult Post([FromBody]Entity entity)
        {
            _context.Entities.Add(entity);
            _context.SaveChanges();
            return Created(entity);
        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody]Entity entity)
        {
            if (key != entity.Id)
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                return BadRequest(exception);
            }

            return NoContent();
        }

        [EnableQuery]
        public IActionResult Delete(int key)
        {
            Entity entity = _context.Entities.FirstOrDefault(entity => entity.Id == key);

            if(entity == null)
            {
                return NotFound();
            }

            _context.Entities.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}
