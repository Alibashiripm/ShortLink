using ShortLink.Domain.Models.Account;
using ShortLink.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLink.Domain.Interface
{
    public interface IUserRepository:IAsyncDisposable
    {
        Task AddUser(User user);
        Task<bool> IsExistMobile(string mobile);
        Task<User> GetUserByMobile(string mobile);
        Task<List<UserForShowViewModel>> GetAllUserForShow();
        Task<User> GetUserById(long userId);
        void UpdateUser(User user);
        Task SaveChange();
    }
}
