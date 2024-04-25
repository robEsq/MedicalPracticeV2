using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;

namespace MPv2_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase {
        _MPv2DbContext dbContext;

        // Get all appointments
        [HttpGet]
        public async Task<ActionResult<List<AppointmentPractitionerPatient>>> GetAllAppointments() {
            using (dbContext = new _MPv2DbContext()) {
                var appointments = await dbContext.AppointmentPractitionerPatients
                    .Include(a => a.App)
                    .Include(p => p.Practitioner)
                    .ThenInclude(pu => pu.User)
                    .Include(u => u.Patient)
                    .ToListAsync();
                return Ok(appointments);
            }
        }

        // Get all users
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentPractitionerPatient>> GetAppointment(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var appointment = dbContext.AppointmentPractitionerPatients
                    .Include(a => a.App)
                    .Include(p => p.Practitioner)
                    .ThenInclude(pu => pu.User)
                    .Include(u => u.Patient)
                    .FirstOrDefault(ap => ap.AppId == id);
                return Ok(appointment);
            }
        }

        // Delete apppointment
        [HttpDelete]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id) {
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
