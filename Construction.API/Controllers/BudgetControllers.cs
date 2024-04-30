using Construction.API.Data;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("/api/budgets")]
    public class BudgetControllers: ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public BudgetControllers(DataContext context)
        {
            _context = context;
        }

        //Metodo Get - Lista (Read all)
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Budgets.ToListAsync());
        }

        //Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Budget budget)
        {
            _context.Add(budget);
            await _context.SaveChangesAsync();
            return Ok(budget);
        }

        //Get por ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var budget = await _context.Budgets.FirstOrDefaultAsync
                (x => x.Id == id);

            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
        }

        //Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Budget budget)
        {
            _context.Update(budget);
            await _context.SaveChangesAsync();
            return Ok(budget);
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var budget = await _context.Budgets.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (budget == null)
            {
                return NotFound();
            }
            _context.Remove(budget);
            await _context.SaveChangesAsync();          

            return NoContent();
        }
    }
}

