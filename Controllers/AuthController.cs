// .\Controllers\AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Services;
using Data;
using Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService;
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(AuthenticationService authService, UserRepository userRepository, IConfiguration configuration)
    {
        _authService = authService;
        _userRepository = userRepository;
        _configuration = configuration;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var isValid = _authService.ValidateCredentials(request.Username, request.Password);
        if (!isValid)
        {   
            Console.WriteLine("User does not have valid credentials");
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken
        (
            issuer: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        var user = _userRepository.Get(request.Username);

        user.Token = tokenString;
        _userRepository.Update(user);

        return Ok(new { Token = tokenString });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User newUser)
    {
        if (!EmailValidator.IsValidEmail(newUser.Email))
        {
            return BadRequest("Invalid email format");
        }
        var existingUser = _userRepository.Exists(newUser.Username,newUser.Email);

        if (existingUser)
        {
            return Conflict("A user with the same username or email already exists");
        }

        newUser.Password = _authService.HashPassword(newUser.Password);
        _userRepository.Add(newUser);

        return Ok();
    }

}