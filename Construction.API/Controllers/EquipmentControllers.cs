//Controllers for Equipments
using Construction.API.Data;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction.API.Controllers
{
    //Route for the api Methods
    [ApiController]
    [Route("/api/Equipments")]
    public class EquipmentControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public EquipmentControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Equipments.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Equipment equipment)
        {
            _context.Add(equipment);
            await _context.SaveChangesAsync();
            return Ok(equipment);
        }

        //MethodGet by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var equipment = await _context.Equipments.FirstOrDefaultAsync
                (x => x.Id == id);

            if (equipment == null)
            {
                return NotFound();
            }
            return Ok(equipment);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Equipment equipment)
        {
            _context.Update(equipment);
            await _context.SaveChangesAsync();
            return Ok(equipment);
        }

        //Method Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var deletedrows = await _context.Equipments.Where(x => x.Id == id).
                ExecuteDeleteAsync();

            if (deletedrows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}