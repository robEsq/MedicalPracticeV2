using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPv2_API.Models;
using System.Collections;
using System.Text.Json.Serialization;

namespace MPv2_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private _MPv2DbContext dbContext;

        // Get all users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers() {
            using (dbContext = new _MPv2DbContext()) {
                var users = await dbContext.Users.ToListAsync();
                return Ok(users);
            }
        }

        // Get user by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var user = await dbContext.Users.FindAsync(id);

                if (user is null) {
                    return NotFound($"User with ID '{id}' not found!");
                }

                return Ok(user);
            }
        }

        // Create new user
        // Ask about Data Transfer Objects DTO, then convert this to it
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user) {
            using (dbContext = new _MPv2DbContext()) {
                // This stops unintentional overwriting of existing records in User
                foreach (User u in this.dbContext.Users.ToList()) {
                    if (user.UserId == u.UserId) {
                        return BadRequest($"UserId with ID '{user.UserId}' already exists!");
                    }
                }

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                
                return Ok($"User {user.FName} {user.LName} added!");
            }
        }
        
        // Update existing user
        // Ask about Data Transfer Objects DTO, then convert this to it
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(int id, User upadatedUser) {
            using (dbContext = new _MPv2DbContext()) {
                // Think about putting updatedUser in a temp ArrayList to copy data

                var dbUser = await dbContext.Users.FindAsync(id);

                if (dbUser is null) {
                    return NotFound($"User with ID '{id}' not found!");
                }

                dbUser.Title = upadatedUser.Title;
                dbUser.FName = upadatedUser.FName;
                dbUser.MiddleInitial = upadatedUser.MiddleInitial;
                dbUser.LName = upadatedUser.LName;
                dbUser.MedicareNo = upadatedUser.MedicareNo;
                dbUser.HomePhoneNo = upadatedUser.HomePhoneNo;
                dbUser.MobilePhoneNo = upadatedUser.MobilePhoneNo;
                dbUser.DateOfBirth = upadatedUser.DateOfBirth;
                dbUser.Gender = upadatedUser.Gender;

                await dbContext.SaveChangesAsync();

                return Ok(await GetUser(id));
            }
        }

        // Delete user
        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUser(int id) {
            using (dbContext = new _MPv2DbContext()) {
                var dbUser = await dbContext.Users.FindAsync(id);

                if (dbUser is null) {
                    return NotFound($"User with ID '{id}' not found!");
                }

                dbContext.Users.Remove(dbUser);
                await dbContext.SaveChangesAsync();

                return Ok($"User with ID {id} removed from database!");
            }
        }
    }
}
