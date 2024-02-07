using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Data;
using Training.model;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private TrainingsApiDBContext dbContext;
        public UsersController(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await dbContext.Users.ToListAsync());

        }

        [HttpPost]

        public async Task<IActionResult> AddUsers(User addUserRequest)
        {
            var User = new User()
            {
                // Id = Guid.NewGuid(),
                Name = addUserRequest.Name,
                Password = addUserRequest.Password,
                Username = addUserRequest.Username,
                //TrainingType = addTrainingRequest.TrainingType,


            };
            await dbContext.Users.AddAsync(User);
            await dbContext.SaveChangesAsync();
            return Ok(User);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var User = await dbContext.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, User updateUserRequest)
        {
            var User = await dbContext.Users.FindAsync(id);
            if (User != null)
            {

                User.Name = updateUserRequest.Name;
                User.Username = updateUserRequest.Username;


                await dbContext.SaveChangesAsync();
                return Ok(User);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await dbContext.Users.FindAsync(id);
            if (User != null)
            {
                dbContext.Remove(User);
                dbContext.SaveChanges();
                return Ok(User);
            }
            return NotFound();
        }


    }

}
