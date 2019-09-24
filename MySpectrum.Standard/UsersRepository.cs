using Microsoft.EntityFrameworkCore;
using MySpectrum.Helper;
using MySpectrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MySpectrum.Standard
{
    public class UsersRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UsersRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public async Task<IEnumerable<User>> GetUserAsyc()
        {
            try
            {
                var users = await _databaseContext.Users.ToListAsync();
                return users;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var users = await _databaseContext.Users.FindAsync(id);
                return users;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                var tracking = await _databaseContext.Users.AddAsync(user);
                await _databaseContext.SaveChangesAsync();
                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {

                return false;

            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                var tracking = _databaseContext.Update(user);
                await _databaseContext.SaveChangesAsync();
                var isModified = tracking.State == EntityState.Modified;
                return isModified;
            }
            catch (Exception e)
            {

                return false;

            }
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            try
            {
                var user = _databaseContext.Users.FindAsync(id);
                var tracking = _databaseContext.Remove(user);

                await _databaseContext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;
                return isDeleted;
            }
            catch (Exception e)
            {

                return false;

            }
        }

        public async Task<IEnumerable<User>> QueryUserAsync(Func<User, bool> predicate)
        {
            try
            {
                var users = _databaseContext.Users.Where(predicate);
                return users.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}
