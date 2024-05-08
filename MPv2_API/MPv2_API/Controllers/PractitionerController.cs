using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;
using System.Collections.Concurrent;

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
        public ActionResult<Practitioner> GetPractitioner(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var practitioner = dbContext.Practitioners.Include(x => x.User)
                    .AsNoTracking().FirstOrDefault(p => p.PractitionerId == id);

                if (practitioner is null) {
                    return NotFound($"Practitioner with ID '{id}' not found!");
                }

                return Ok(practitioner);
            }
        }

        // Create new practitioner
        // Find a way to make an overloaded version with an http POST for adding a new user
        // Turns out it already does :)
        [HttpPost]
        public async Task<ActionResult<Practitioner>> AddPractitioner(Practitioner practitioner) {
            using (dbContext = new _MPv2DbContext()) {

                // This stops unintentional overwriting of existing records in Practitioner
                foreach (Practitioner p in this.dbContext.Practitioners.ToList()) {
                    if (practitioner.PractitionerId == p.PractitionerId) {
                        return BadRequest($"UserId with ID '{practitioner.PractitionerId}' already exists!");
                    }
                }

                // This stops unintentional overwriting of existing records in User
                foreach (Practitioner p in this.dbContext.Practitioners.ToList()) {
                    if (practitioner.UserId == p.UserId) {
                        return BadRequest($"UserId with ID '{practitioner.UserId}' already exists!");
                    }
                }

                dbContext.Practitioners.Add(practitioner);
                await dbContext.SaveChangesAsync();

                return Ok($"Practitioner {practitioner.PractitionerId} added!");
            }
        }
        
        // Update existing practitioner
        [HttpPut]
        public async Task<ActionResult<Practitioner>> UpdatePractitioner(int id, Practitioner upadatedPractitioner) {
            using (dbContext = new _MPv2DbContext()) {
                // Think about putting updatedUser in a temp ArrayList to copy data

                var dbPractitioner = await dbContext.Practitioners.FindAsync(id);

                if (dbPractitioner is null) {
                    return NotFound($"Practitioner with ID '{id}' not found!");
                }
                
                // This stops unintentional doubling up of existing records in User
                // Might have to create a list that excludes the UserId in question
                foreach (Practitioner p in this.dbContext.Practitioners.ToList()) {
                    if (upadatedPractitioner.UserId == p.UserId) {
                        return BadRequest($"Practitioner with UserID '{upadatedPractitioner.UserId}' already exists!");
                    }
                }

                dbPractitioner.MedicalRegistrationNo = upadatedPractitioner.MedicalRegistrationNo;
                dbPractitioner.UserId = upadatedPractitioner.UserId;

                await dbContext.SaveChangesAsync();

                return Ok(GetPractitioner(id));
            }
        }

        // Delete practitioner
        [HttpDelete]
        public async Task<ActionResult<Practitioner>> DeletePractitioner(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var dbPractitioner = await dbContext.Practitioners.FindAsync(id);

                if (dbPractitioner is null) {
                    return NotFound($"Practitioner with ID '{id}' not found!");
                }

                dbContext.Practitioners.Remove(dbPractitioner);
                await dbContext.SaveChangesAsync();

                return Ok($"Practitioner with ID {id} removed from database!");
            }
        }
    }
}
