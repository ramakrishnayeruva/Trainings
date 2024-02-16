using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Data;
using DataAccess.model;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentictaionController : ControllerBase
    {
        private TrainingsApiDBContext dbContext;

        public AuthentictaionController(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            var user1 = dbContext.Users.SingleOrDefault(s => s.Username == user.UserName&&s.Password==user.Password);
            
            if (user1 is null)
            {
                return BadRequest("Invalid user request!!!");
            }
             if (user1 != null)            
            {
                var claims = new List<Claim> {                
                 new Claim("Id", user1.Id.ToString()),
                 new Claim("UserName", user1.Name) };
            
                    var userRoles = dbContext.UserRoles.Where(u => u.UserId == user1.Id).ToList();
                var roleIds = userRoles.Select(s => s.RoleId).ToList();
                var roles = dbContext.Roles.Where(r => roleIds.Contains(r.Id)).ToList();                

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(

                    issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    claims,
                    //new List<Claim>(new Claim[] {new Claim(ClaimTypes.Name, user1.Username),new Claim(ClaimTypes.Role,"Trainer")}),
                    //claims: new List<Claim>(ClaimTypes.Name,user.UserName),
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
