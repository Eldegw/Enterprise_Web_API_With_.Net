using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;
using static System.Net.WebRequestMethods;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager ,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto RequestFromUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.UserName = RequestFromUser.UserName;
                appUser.Email = RequestFromUser.Email;

               IdentityResult result = await userManager.CreateAsync(appUser, RequestFromUser.Password);
                if (result.Succeeded)
                {
                    return Ok("Created");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }



            }
            return BadRequest(ModelState);
         
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto RequestFromUser)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser userFromDB = await userManager.FindByNameAsync(RequestFromUser.UserName);
                if (userFromDB != null)
                {
                  bool found = await userManager.CheckPasswordAsync(userFromDB, RequestFromUser.Password);

                    if (found) 
                    {
                        //generate Token

                      List<Claim>userclaims = new List<Claim>();

                        userclaims.Add(new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString()));

                        userclaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                        userclaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));
                         var userRoles =  await userManager.GetRolesAsync(userFromDB);
                        foreach (var roleName in userRoles)
                        { 
                            userclaims.Add(new Claim(ClaimTypes.Role, roleName));
                        }

                        var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecurityKey"]));

                        SigningCredentials signingCred = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);


                        //Design Token

                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            
                            issuer: config["JWT:IssuerIP"],
                            audience: config["JWT:AudienceIP"],
                            expires:DateTime.Now.AddHours(1),
                            claims: userclaims,
                            signingCredentials: signingCred


                        );

                        //Generate Token 

                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = DateTime.Now.AddHours(1), 



                        });



                    }


                }
                ModelState.AddModelError("UserName", "UserName OR Password Is Invalid");


            }
            return BadRequest(ModelState);

        }














    }
}
