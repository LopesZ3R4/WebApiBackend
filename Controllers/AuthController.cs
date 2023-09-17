using Microsoft.AspNetCore.Mvc;
using Services;
using Data;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService;
    private readonly UserRepository _userRepository; // Add this line

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
        newUser.HashedPassword = _authService.HashPassword(newUser.HashedPassword);
        _userRepository.Add(newUser);

        return Ok();
    }

    // Add more methods here as needed.
}