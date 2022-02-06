using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emails.Services
{
    public class FakeUsersService : IUsersService
    {
        private readonly UsersDTO[] _users =
        {
            // Insert Seed Data for the fake
            new UsersDTO{ Id = 1, UName = "Tester123", Email = "t123@gmail.com ", Password = "password1234"},
            new UsersDTO{ Id = 2, UName = "MCaldwell00", Email = "mdcaldwell16@gmail.com", Password = "testpass"},
            new UsersDTO{ Id = 3, UName = "Joe_Bloggs", Email = "JBL@gmail.com", Password = "abcdef"},
            new UsersDTO{ Id = 4, UName = "AABB", Email = "AABB@gmail.com", Password = "AABB"},
        };

        public Task<UsersDTO> GetUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<IEnumerable<UsersDTO>> GetUsersAsync(string email)
        {
            var users = _users.AsEnumerable();
            if(email != null)
            {
                users = users.Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            return Task.FromResult(users);
        }

    }
}
