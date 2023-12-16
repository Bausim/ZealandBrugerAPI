using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZealandBrugerAPI.EDbContext;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZealandBrugerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerController : ControllerBase
    {
        private readonly BrugerDbContext _dbContext;
        public BrugerController(BrugerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<BrugereController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bruger>>> Get()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            return await _dbContext.bruger.ToListAsync();
        }

        // GET api/<BrugereController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bruger>> Get(int id)
        {
            if (_dbContext.Set<Bruger>() == null) 
            {
                return NotFound();            
            }

            var bruger = await _dbContext.bruger.FindAsync(id);

            if (bruger == null)
            {
                return NotFound("No bruger with given Id");
            }

            return Ok(bruger);
        }

        // POST api/<BrugereController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BrugereController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BrugereController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
