//Controllers for MaterialAssignments
using Construction.API.Data;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction.API.Controllers
{
    //Route for the api Methods
    [ApiController]
    [Route("/api/MaterialAssignments")]
    public class MaterialAssignmentControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public MaterialAssignmentControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.MaterialAssignments.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(MaterialAssignment materialassignment)
        {
            _context.Add(materialassignment);
            await _context.SaveChangesAsync();
            return Ok(materialassignment);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var materialassignment = await _context.MaterialAssignments.FirstOrDefaultAsync
                (x => x.Id == id);

            if (materialassignment == null)
            {
                return NotFound();
            }
            return Ok(materialassignment);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(MaterialAssignment materialassignment)
        {
            _context.Update(materialassignment);
            await _context.SaveChangesAsync();
            return Ok(materialassignment);
        }

        //Method Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var deletedrows = await _context.MaterialAssignments.Where(x => x.Id == id).
                ExecuteDeleteAsync();

            if (deletedrows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
