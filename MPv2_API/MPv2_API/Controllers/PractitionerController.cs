using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;

namespace MPv2_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PractitionerController : ControllerBase {
        private _MPv2DbContext dbContext;

        // Get all practitioners
        [HttpGet]
        public async Task<ActionResult<List<Practitioner>>> GetAllPractitioners() {
            using (dbContext = new _MPv2DbContext()) {
                var practitioners = await dbContext.Practitioners.Include(x => x.User).ToListAsync();

                return Ok(practitioners);
            }
        }

        // Get practitioner by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Practitioner>> GetPractitioner(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var practitioner = dbContext.Practitioners.Include(x => x.User).AsNoTracking().FirstOrDefault(m => m.PractitionerId == id);

                if (practitioner is null) {
                    return NotFound($"Practitioner with ID '{id}' not found!");
                }

                return Ok(practitioner);
            }
        }


        /*
                // Create new Practitioner
                // need check to see if user already is a practitioner
                [HttpPost]
                public async Task<ActionResult<List<Practitioner>>> AddPractitioner(User user, Practitioner practitioner) {
                    using (dbContext = new _MPv2DbContext()) {
                        UserController uc = new UserController();
                        await uc.AddUser(user); // Adds new user information

                        await dbContext.SaveChangesAsync();

                        return Ok(await GetAllPractitioners());
                    }
                }
        */
    }
}
