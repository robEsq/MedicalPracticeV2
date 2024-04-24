using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;

namespace MPv2_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DayController : ControllerBase {// Get days and the corresponding available practitioner(s)
        _MPv2DbContext dbContext; 
        
        [HttpGet("Availability")]
        public async Task<ActionResult<List<Day>>> GetPractitionerAvailability() {
            using (dbContext = new _MPv2DbContext()) {
                var days = await dbContext.Days.Include(x => x.Practitioners).ThenInclude(s => s.User)
                    .ToListAsync();

                return Ok(days);
            }
        }
    }
}
