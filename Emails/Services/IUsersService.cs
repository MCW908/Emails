using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emails.Services
{
    public interface IUsersService
    {
        Task<UsersDTO> GetUserAsync(int id);

        Task<IEnumerable<UsersDTO>> GetUsersAsync(string email);
    }
}
