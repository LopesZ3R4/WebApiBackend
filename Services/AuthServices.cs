// .\Services\AuthServices
using System.Security.Cryptography;
using System.Text;
using Data;

namespace Services {
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;

    public AuthenticationService(UserRepository userRepository)
    {
        _userRepository = userRepository;
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
    }
}