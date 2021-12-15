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
            // Insert Seed Data
            new UsersDTO{ Id = 1, UName = "Tester123", Email = "", Password = ""},
            new UsersDTO{ Id = 2, UName = "MCaldwell00", Email = "", Password = ""},
            new UsersDTO{ Id = 3, UName = "Joe_Bloggs", Email = "", Password = ""},
            new UsersDTO{ Id = 4, UName = "AABB", Email = "", Password = ""},
            new UsersDTO{ Id = 5, UName = "", Email = "", Password = ""},
            new UsersDTO{ Id = 6, UName = "", Email = "", Password = ""}
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
