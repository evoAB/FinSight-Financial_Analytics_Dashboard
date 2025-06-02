using FinanceDashboard.Data;
using FinanceDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        var exists = _context.Users.Any(u => u.Username == dto.Username);
        if (exists) return BadRequest("User already exists");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered successfully");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return Unauthorized("Invalid credentials");

        var token = JwtHelper.GenerateJwtToken(user, _config);
        return Ok(new { token });
    }

    [Authorize]
    [HttpGet("secure")]
    public IActionResult SecureEndpoint()
    {
        return Ok("You are authenticated!");
    }
}
