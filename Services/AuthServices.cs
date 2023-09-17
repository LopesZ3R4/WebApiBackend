// File Path: c:\WebApiBackend\Services\AuthServices
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

    public bool ValidateCredentials(string username, string password)
    {
        Console.WriteLine("Initializing Credentials Validation");
        var user = _userRepository.Get(username);

        if (user == null)
        {   
            Console.WriteLine($"No user found with username: {username}");
            return false;
        }

        var hashedPassword = HashPassword(password);

        return hashedPassword == user.HashedPassword;
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