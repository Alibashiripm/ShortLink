using ShortLink.Application.DTOs.Account;
using ShortLink.Domain.Models.Account;
using ShortLink.Domain.ViewModels.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLink.Application.Interfaces
{
    public interface IUserService
    {
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO registerUser);
        Task<LoginUserResult> LoginUser(LoginUserDTO loginUser);
        Task<User> GetUserByMobile(string mobile);
        Task<List<UserForShowViewModel>> GetAllUserForShow();
        Task<EditUserResult> EditUser(EditUserDTO editUser);
        Task<EditUserDTO> GetEditUserByAdmin(long userId);
        Task<CreateUserResult> AddUser(CreateUserDTO craeteUser);
    }
}
