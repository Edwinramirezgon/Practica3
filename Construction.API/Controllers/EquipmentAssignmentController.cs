//Controllers for EquipmentAssignments
using Construction.API.Data;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction.API.Controllers

{   //Route for the api Methods
    [ApiController]
    [Route("/api/EquipmentAssignments")]
    public class EquipmentAssignmentController : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public EquipmentAssignmentController(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.EquipmentAssignments.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(EquipmentAssignment equipmentassignment)
        {
            _context.Add(equipmentassignment);
            await _context.SaveChangesAsync();
            return Ok(equipmentassignment);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var equipmentassignment = await _context.EquipmentAssignments.FirstOrDefaultAsync
                (x => x.Id == id);

            if (equipmentassignment == null)
            {
                return NotFound();
            }
            return Ok(equipmentassignment);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(EquipmentAssignment equipmentassignment)
        {
            _context.Update(equipmentassignment);
            await _context.SaveChangesAsync();
            return Ok(equipmentassignment);
        }

        //Method Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var deletedrows = await _context.EquipmentAssignments.Where(x => x.Id == id).
                ExecuteDeleteAsync();

            if (deletedrows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
