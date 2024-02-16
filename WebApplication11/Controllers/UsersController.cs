using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.model;
using Business.Interface;
using Business.Service;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private TrainingsApiDBContext dbContext;
        private IUser _IUser;
        public UsersController(TrainingsApiDBContext dbContext,IUser IUser)
        {
            this.dbContext = dbContext;
            this._IUser = IUser;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await Task.FromResult(_IUser.GetUsers());
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> AddUsers(User addUserRequest)
        {
            _IUser.AddUsers(addUserRequest);
            //return await Task.FromResult(CreatedAtAction("GetEmployees", new { id = employee.EmployeeID }, employee));
            return await Task.FromResult(addUserRequest);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var users = await Task.FromResult(_IUser.GetUsers(id));
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                _IUser.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(user);
        }
        private bool UserExists(int id)
        {
            return _IUser.CheckUser(id);
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var employee = _IUser.DeleteUser(id);
            return await Task.FromResult(employee);
        }



    }

}
