//Controllers for Construction Team
using Construction.API.Data;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction.API.Controllers

{   //Route for the api Methods
    [ApiController]
    [Route("/api/constructionteams")]
    public class ConstructionTeamControllers : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public ConstructionTeamControllers(DataContext context)
        {
            _context = context;
        }

        //Method Get - List (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.ConstructionTeams.ToListAsync());
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(ConstructionTeam constructionteam)
        {
            _context.Add(constructionteam);
            await _context.SaveChangesAsync();
            return Ok(constructionteam);
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var constructionteam = await _context.ConstructionTeams.FirstOrDefaultAsync
                (x => x.Id == id);

            if (constructionteam == null)
            {
                return NotFound();
            }
            return Ok(constructionteam);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(ConstructionTeam constructionteam)
        {
            _context.Update(constructionteam);
            await _context.SaveChangesAsync();
            return Ok(constructionteam);
        }

        //Method Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var deletedrows = await _context.ConstructionTeams.Where(x => x.Id == id).
                ExecuteDeleteAsync();

            if (deletedrows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}