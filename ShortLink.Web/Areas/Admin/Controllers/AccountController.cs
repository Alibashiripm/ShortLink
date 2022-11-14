using Microsoft.AspNetCore.Mvc;
using ShortLink.Application.DTOs.Account;
using ShortLink.Application.Interfaces;
using System.Threading.Tasks;

namespace ShortLink.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        #region constractor
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region user list
        [HttpGet("user-list")]
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllUserForShow());
        }
        #endregion

        #region edit user
        [HttpGet("edit-user/{userId}")]
        public async Task<IActionResult> EditUser(long userId)
        { 
            return View(await _userService.GetEditUserByAdmin(userId));
        }

        [HttpPost("edit-user/{userId}")]
        public async Task<IActionResult> EditUser(EditUserDTO editUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditUser(editUser);
                switch (result)
                {
                    case EditUserResult.NotFound:
                        TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        break;
                    case EditUserResult.Success:
                        TempData[SuccessMessage] = $"کاربر {editUser.LastName} با موفقیت ویرایش شد  ";
                        return RedirectToAction("Index");
                }
            }

            return View(editUser);
        }
        #endregion

        #region create user
        [HttpGet("craete-user")]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost("craete-user"),ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserDTO craeteUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddUser(craeteUser);
                switch (result)
                {
                    case CreateUserResult.Error:
                        TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری میباشد";
                        break;
                    case CreateUserResult.Success:
                        TempData[SuccessMessage] = "عملیات افزودن کاربر با موفقیت انجام شد";
                        return RedirectToAction("Index");
                }
            }

            return View(craeteUser);
        }
        #endregion
    }
}
