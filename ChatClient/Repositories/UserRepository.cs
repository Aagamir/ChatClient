using ChatClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _context;

        public UserRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id, CancellationToken cancellationToken)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}