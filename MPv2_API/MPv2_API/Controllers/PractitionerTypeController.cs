using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;

namespace MPv2_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PractitionerTypeController : ControllerBase {
        _MPv2DbContext dbContext;

        // Get disciplines and the corresponding practitioner(s)
        [HttpGet]
        public async Task<ActionResult<List<PractitionerType>>> GetPractitionerType() {
            using (dbContext = new _MPv2DbContext()) {
                var types = await dbContext.PractitionerTypes.Include(x => x.Practitioners).ThenInclude(s => s.User)
                    .ToListAsync();

                return Ok(types);
            }
        }
    }
}
