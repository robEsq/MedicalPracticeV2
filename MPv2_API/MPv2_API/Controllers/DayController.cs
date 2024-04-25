using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;
using System.Collections.Concurrent;

namespace MPv2_API.Controllers {
    [Route("api/Availability")]
    [ApiController]
    public class DayController : ControllerBase {
        _MPv2DbContext dbContext;

        // Get days and the corresponding available practitioner(s)
        [HttpGet()]
        public async Task<ActionResult<List<Day>>> GetPractitionerAvailabilities() {
            using (dbContext = new _MPv2DbContext()) {
                var days = await dbContext.Days.Include(x => x.Practitioners).ThenInclude(s => s.User)
                    .ToListAsync();

                return Ok(days);
            }
        }

        // Get specific days and the corresponding available practitioner(s)
        [HttpGet("{id}")]
        public async Task<ActionResult<Day>> GetPractitionerAvailability(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var day = dbContext.Days.Include(x => x.Practitioners).ThenInclude(s => s.User)
                    .AsNoTracking().FirstOrDefault(d => d.DayId == id);

                if (day is null) {
                    return NotFound($"Day with ID '{id}' not found!");
                }

                return Ok(day);
            }
        }
    }
}
