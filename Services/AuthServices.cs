// .\Services\AuthServices
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Data;
using Microsoft.IdentityModel.Tokens;

namespace Services {
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

    public AuthenticationService(UserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public bool ValidateCredentials(string Username, string password)
    {
        Console.WriteLine("Initializing Credentials Validation");
        var user = _userRepository.Get(Username);

        if (user == null)
        {   
            Console.WriteLine($"No user found with user: {Username}");
            return false;
        }

        var hashedPassword = HashPassword(password);

        return hashedPassword == user.Password;
    }

    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
    public string GetUsernameFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidateAudience = false
        };

        var claims = handler.ValidateToken(token, validations, out _);
        var usernameClaim = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        return usernameClaim?.Value;
    }
    }
}