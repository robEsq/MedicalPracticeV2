using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;

namespace MPv2_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PractitionerTypeController : ControllerBase {
        _MPv2DbContext dbContext;

/*
        // Get disciplines and the corresponding practitioner(s)
        [HttpGet]
        public async Task<ActionResult<List<PractitionerType>>> GetPractitionerTypes() {
            using (dbContext = new _MPv2DbContext()) {
                var types = await dbContext.PractitionerTypes.Include(x => x.Practitioners).ThenInclude(s => s.User)
                    .ToListAsync();

                return Ok(types);
            }
        }

        // Get specific disciplines and the corresponding practitioner(s)
        [HttpGet("{id}")]
        public async Task<ActionResult<Day>> GetPractitionerType(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var type = dbContext.PractitionerTypes.Include(x => x.Practitioners).ThenInclude(s => s.User)
                    .AsNoTracking().FirstOrDefault(pt => pt.PracTypeId == id);

                if (type is null) {
                    return NotFound($"Practitioner type with ID '{id}' not found!");
                }

                return Ok(type);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PractitionerType>> AddPractitioner(PractitionerType practitionerType) {
            using (dbContext = new _MPv2DbContext()) {

                // This stops unintentional overwriting of existing records in Practitioner
                foreach (PractitionerType p in this.dbContext.PractitionerTypes.ToList()) {
                    if (practitionerType.PracTypeId == p.PracTypeId) {
                        return BadRequest($"UserId with ID '{practitionerType.PracTypeId}' already exists!");
                    }
                }

                dbContext.PractitionerTypes.Add(practitionerType);
                await dbContext.SaveChangesAsync();

                return Ok($"Practitioner {practitionerType.PracTypeId} added!");
            }
        }

        // Update existing practitioner type
        [HttpPut]
        public async Task<ActionResult<PractitionerType>> UpdatePractitionerType(int id, PractitionerType upadatedPractitionerType) {
            using (dbContext = new _MPv2DbContext()) {

                var dbPractitionerType = await dbContext.PractitionerTypes.FindAsync(id);

                if (dbPractitionerType is null) {
                    return NotFound($"Practitioner type with ID '{id}' not found!");
                }

                // This stops unintentional doubling up of existing records in PractitionerType
                foreach (PractitionerType p in this.dbContext.PractitionerTypes.ToList()) {
                    if (upadatedPractitionerType.PracTypeId == p.PracTypeId) {
                        return BadRequest($"Practitioner with UserID '{upadatedPractitionerType.PracTypeId}' already exists!");
                    }
                }

                dbPractitionerType.PracTypeName = upadatedPractitionerType.PracTypeName;

                await dbContext.SaveChangesAsync();

                return Ok(await GetPractitionerType(id));
            }
        }

        // Delete practitioner
        [HttpDelete]
        public async Task<ActionResult<PractitionerType>> DeletePractitionerType(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var dbPractitionerType = await dbContext.PractitionerTypes.FindAsync(id);

                if (dbPractitionerType is null) {
                    return NotFound($"Practitioner type with ID '{id}' not found!");
                }

                dbContext.PractitionerTypes.Remove(dbPractitionerType);
                await dbContext.SaveChangesAsync();

                return Ok($"Practitioner type with ID {id} removed from database!");
            }
        }
*/
    }
}
