// File Path: c:\WebApiBackend\Data\UserRepository.cs

using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? Get(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}