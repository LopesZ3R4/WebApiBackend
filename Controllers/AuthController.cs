// .\Controllers\AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Services;
using Data;
using Utils;

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

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var isValid = _authService.ValidateCredentials(request.Username, request.Password);
        if (!isValid)
        {   
            Console.WriteLine("User does not have valid credentials");
            return Unauthorized();
        }

        Console.WriteLine("User authenticated successfully");
        return Ok();
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