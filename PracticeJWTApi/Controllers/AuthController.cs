using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Practice.Domain.Identity;
using PracticeJWTApi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticeJWTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterVM registerVM, string roleName="User") 
        {

            ApiResponse<ApplicationUser> response;

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registerVM.UserName,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber
                };

                var returnUser = await _userManager.CreateAsync(user, registerVM.Password);

                if (!returnUser.Succeeded)
                {
                    foreach (IdentityError error in returnUser.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }

                    return BadRequest(returnUser.Errors);
                }

                ApplicationRole applicationRole = await _roleManager.FindByNameAsync(roleName);

                if (applicationRole == null)
                {
                    applicationRole = new ApplicationRole() { Name = roleName };
                    await _roleManager.CreateAsync(applicationRole);
                }

                await _userManager.AddToRoleAsync(user, applicationRole.Name);

                response = new ApiResponse<ApplicationUser>(true, "Success", user);

            }
            catch(Exception ex)
            {
                response = new ApiResponse<ApplicationUser>(false, ex.Message, null);
            }

            return Ok(response);
        }


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInVM signInVM)
        {
            var user = await GetUserByEmailOrUserName(signInVM.EmailOrUserName);

            if(user != null)
            {
                await _signInManager.PasswordSignInAsync(user, signInVM.Password, signInVM.RememberMe, lockoutOnFailure: false);

                var token = GenerateJwtToken(user);
               
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return Ok(token);
                }

                return Ok(new { token });
            }else
            {
                return Unauthorized();
            }
        }

        private async Task<ApplicationUser> GetUserByEmailOrUserName(string emailOrUserName)
        {
            return emailOrUserName.Contains("@")
                ? await _userManager.FindByEmailAsync(emailOrUserName)
                : await _userManager.FindByNameAsync(emailOrUserName);
        }

        [HttpGet("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok("SignOut");
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpirationMinutes"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }





    }
}
