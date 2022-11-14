using Microsoft.EntityFrameworkCore;
using ShortLink.Domain.Interface;
using ShortLink.Domain.Models.Account;
using ShortLink.Domain.ViewModels.Account;
using ShortLink.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortLink.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region constractor
        private readonly ShortLinkContext _context;
        public UserRepository(ShortLinkContext context)
        {
            _context = context;
        }

        #endregion

        #region account
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task<bool> IsExistMobile(string mobile)
        {
            return await _context.Users.AnyAsync(u => u.Mobile == mobile);
        }
        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Mobile == mobile);
        }
        public async Task<List<UserForShowViewModel>> GetAllUserForShow()
        {
            var allUser = await _context.Users.AsQueryable()
                .Select(u => new UserForShowViewModel
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Mobile = u.Mobile,
                    IsAdmin = u.IsAdmin,
                    IsBlocked = u.IsBlocked,
                }).ToListAsync();

            return allUser;
        }
        public async Task<User> GetUserById(long userId)
        {
            return await _context.Users.AsQueryable()
                .SingleOrDefaultAsync(u => u.Id == userId);
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
        #endregion

        #region dispose & save change
        public async ValueTask DisposeAsync()
        {
            if (_context != null) await _context.DisposeAsync();
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }


        #endregion
    }
}
