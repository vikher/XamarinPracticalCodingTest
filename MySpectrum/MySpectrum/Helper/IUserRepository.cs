using MySpectrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySpectrum.Helper
{
   public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserAsyc();

        Task<User> GetUserByIdAsync(int id);

        Task<bool> AddUserAsync(User User);

        Task<bool> UpdateUserAsync(User User);

        Task<bool> RemoveUserAsync(int id);

        Task<IEnumerable<User>> QueryUserAsync(Func<User, bool> predicate);

    }
}
