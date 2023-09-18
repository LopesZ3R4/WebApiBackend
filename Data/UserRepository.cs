// .\Data\UserRepository.cs

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

        public User? Get(string UsernameOrMail)
        {
            return _context.Users.FirstOrDefault(u => u.Username == UsernameOrMail || u.Email == UsernameOrMail);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}