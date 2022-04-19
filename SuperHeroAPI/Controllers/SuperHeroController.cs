using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            var superHeroesFromDb = await _context.SuperHeroes.ToListAsync();
            return Ok(superHeroesFromDb);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var superHeroFromDb = await _context.SuperHeroes.FirstOrDefaultAsync(x => x.Id == id);

            if (superHeroFromDb == null)
                return BadRequest("Hero not found!");

            return Ok(superHeroFromDb);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Add(SuperHero superHero)
        {
            await _context.SuperHeroes.AddAsync(superHero);
            await _context.SaveChangesAsync();

            var superHeroesFromDb = await _context.SuperHeroes.ToListAsync();
            return Ok(superHeroesFromDb);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Update(SuperHero superHero)
        {
            var superHeroFromDb = await _context.SuperHeroes.FirstOrDefaultAsync(x => x.Id == superHero.Id);

            if (superHeroFromDb == null)
                return BadRequest("Hero not found!");

            superHeroFromDb.Name = superHero.Name;
            superHeroFromDb.FirstName = superHero.FirstName;
            superHeroFromDb.LastName = superHero.LastName;
            superHeroFromDb.Place = superHero.Place;

            await _context.SaveChangesAsync();

            var superHeroesFromDb = await _context.SuperHeroes.ToListAsync();
            return Ok(superHeroesFromDb);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var superHeroFromDb = await _context.SuperHeroes.FirstOrDefaultAsync(x => x.Id == id);

            if (superHeroFromDb == null)
                return BadRequest("Hero not found!");

            _context.SuperHeroes.Remove(superHeroFromDb);
            await _context.SaveChangesAsync();

            var superHeroesFromDb = await _context.SuperHeroes.ToListAsync();
            return Ok(superHeroesFromDb);
        }
    }
}
