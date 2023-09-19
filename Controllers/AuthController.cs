// .\Controllers\AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Services;
using Data;
using Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService;
    private readonly UserRepository _userRepository;

    public AuthController(AuthenticationService authService, UserRepository userRepository) // Modify this line
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public IActionResult Login([FromBody] LoginRequest request)
    {
        var isValid = _authService.ValidateCredentials(request.Username, request.Password);
        if (!isValid)
        {   
            Console.WriteLine("User does not have valid credentials");
            return Unauthorized();
        }

        // Create a claim
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Generate token
        var token = new JwtSecurityToken
        (
            issuer: "your_issuer",
            audience: "your_audience",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key")), SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Retrieve user from database
        var user = _userRepository.Get(request.Username);

        // Assign token to user and save changes
        user.Token = tokenString;
        _userRepository.Update(user); // You'll need to implement this method in your UserRepository

        // Send token to client
        return Ok(new { Token = tokenString });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User newUser)
    {
        if (!EmailValidator.IsValidEmail(newUser.Email))
        {
            return BadRequest("Invalid email format");
        }
        var existingUserWithSameUsername = _userRepository.Get(newUser.Username);
        var existingUserWithSameEmail = _userRepository.Get(newUser.Email);

        if (existingUserWithSameUsername != null || existingUserWithSameEmail != null)
        {
            return Conflict("A user with the same username or email already exists");
        }

        newUser.HashedPassword = _authService.HashPassword(newUser.HashedPassword);
        _userRepository.Add(newUser);

        return Ok();
    }

}