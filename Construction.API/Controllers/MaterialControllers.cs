//Controllers for Materials
using Construction.API.Data;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction.API.Controllers
{
    //Route for the api Methods
    [ApiController]
    [Route("/api/Materials")]
    public class MaterialControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public MaterialControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Materials.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Material material)
        {
            _context.Add(material);
            await _context.SaveChangesAsync();
            return Ok(material);
        }

        //Method Get por ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var equipment = await _context.Materials.FirstOrDefaultAsync
                (x => x.Id == id);

            if (equipment == null)
            {
                return NotFound();
            }
            return Ok(equipment);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Material material)
        {
            _context.Update(material);
            await _context.SaveChangesAsync();
            return Ok(material);
        }

        //Method Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var deletedrows = await _context.Materials.Where(x => x.Id == id).
                ExecuteDeleteAsync();

            if (deletedrows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}