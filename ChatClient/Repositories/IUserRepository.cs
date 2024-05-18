using ChatClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user, CancellationToken cancellationToken);

        Task<User> GetUserById(int id, CancellationToken cancellationToken);

        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}