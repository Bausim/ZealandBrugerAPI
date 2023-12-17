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
        public ActionResult Post(Bruger bruger)
        {
            if (bruger == null)
            {
                return BadRequest(bruger);
            }
            if (bruger.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            _dbContext.SaveChangesAsync();
            if (!ModelState.IsValid) 
            {
                return BadRequest("Invalid Data!");
            }

            // Assuming you have configured DbContextOptions in your application's startup
            var optionsBuilder = new DbContextOptionsBuilder<BrugerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=mssql11.unoeuro.com;Initial Catalog=zealandid_dk_db_test;User ID=zealandid_dk;Password=4tn2gwfADdeRB5EGzm6b;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            using (var ctx = new  BrugerDbContext(optionsBuilder.Options))
            {
                ctx.bruger.Add(new Bruger()
                {
                    Id = bruger.Id,
                    Admin = bruger.Admin,
                    Brugernavn = bruger.Brugernavn,
                    Password = bruger.Password,
                });

                ctx.SaveChangesAsync();
            }

            return Ok();

        }

        // PUT api/<BrugereController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Bruger>> Put(int id, Bruger updateBruger)
        {
            if (updateBruger == null || id != updateBruger.Id ) 
            {
                return BadRequest("Invalid data or mismatched IDs");
            }

            try
            {

                var existingBruger = await _dbContext.bruger.FindAsync(id);

                if (existingBruger == null)
                {
                    return NotFound("Bruger not found");
                }
                
                existingBruger.Id = updateBruger.Id;
                existingBruger.Admin = updateBruger.Admin;
                existingBruger.Brugernavn = updateBruger.Brugernavn;
                existingBruger.Password = updateBruger.Password;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // DELETE api/<BrugereController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleteBruger = await _dbContext.bruger.FindAsync(id);
                if (deleteBruger == null)
                {
                    return NotFound();
                }

                var brugerRecords = _dbContext.bruger.Where(b => b.Id == id).ToList();
                _dbContext.bruger.RemoveRange(brugerRecords);

                _dbContext.bruger.Remove(deleteBruger);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
