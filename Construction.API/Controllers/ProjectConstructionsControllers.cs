//Controllers for projectconstructions
using Construction.API.Data;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction.API.Controllers
{
    //Route for the api Methods
    [ApiController]
    [Route("/api/projectconstructions")]
    public class ProjectConstructionsControllers : ControllerBase
    {
        private readonly DataContext _context;

        //Constructor
        public ProjectConstructionsControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.ProjectConstructions.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(ProjectConstruction projectconstruction)
        {
            _context.Add(projectconstruction);
            await _context.SaveChangesAsync();
            return Ok(projectconstruction);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var projectconstruction = await _context.ProjectConstructions.FirstOrDefaultAsync
                (x => x.Id == id);

            if (projectconstruction == null)
            {
                return NotFound();
            }
            return Ok(projectconstruction);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(ProjectConstruction projectconstruction)
        {
            _context.Update(projectconstruction);
            await _context.SaveChangesAsync();
            return Ok(projectconstruction);
        }

        //Method Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var deletedrows = await _context.ProjectConstructions.Where(x => x.Id == id).
                ExecuteDeleteAsync();

            if (deletedrows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

