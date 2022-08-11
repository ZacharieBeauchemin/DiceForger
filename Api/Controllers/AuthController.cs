using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Database;
using Api.Models;

namespace LePapetier_Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase {
    private readonly DiceForgerDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(DiceForgerDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager) {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [Route("token")]
    [HttpPost]
    public async Task<IActionResult> CreateToken(string email, string password) {
        if (await IsValidEmailAndPassword(email, password)) {
            return new ObjectResult(await GenerateToken(email));
        } else {
            return BadRequest();
        }
    }

    [Route("register")]
    [HttpPost]
    public async Task<bool> Register(NewUserModel newUser) {
        if (ModelState.IsValid) {
            IdentityUser user = new() { UserName = newUser.Username, Email = newUser.Email };
            IdentityResult userCreationResult = await _userManager.CreateAsync(user, newUser.Password);

            return userCreationResult.Succeeded;
        } else {
            return false;
        }
    }

    [NonAction]
    private async Task<bool> IsValidEmailAndPassword(string email, string password) {
        IdentityUser? user = await _userManager.FindByEmailAsync(email);
        return await _userManager.CheckPasswordAsync(user, password);
    }

    [NonAction]
    private async Task<dynamic> GenerateToken(string email) {
        IdentityUser? user = await _userManager.FindByEmailAsync(email);

        var roles = from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id where ur.UserId == user.Id select new { ur.UserId, ur.RoleId, r.Name };

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
        };

        foreach (var role in roles) {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        JwtSecurityToken? token = new JwtSecurityToken(
            new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Temp8rpum4ATemp%Q$Nc$Temp")),
                    SecurityAlgorithms.HmacSha256)),
            new JwtPayload(claims)
        );

        var output = new {
            Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserName = email
        };

        return output;
    }
}