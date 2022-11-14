using Microsoft.AspNetCore.Mvc;
using ShortLink.Application.DTOs.Account;
using ShortLink.Application.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;


namespace ShortLink.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region constractor
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region user register
        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
           
            return View();
        }

        [HttpPost("register"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO registerUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(registerUser);
                switch (result)
                {
                    case RegisterUserResult.IsMobileExist:
                        TempData[ErrorMessage] = "The Phone Number Is Dupliceate";
                        ModelState.AddModelError("Mobile", "The Phone Number Is Dupliceate");
                        break;
                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "Your Registertion Is Successful";
                        return Redirect("/");
                }
            }
            return View(registerUser);
        }
        #endregion

        #region user login
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("login"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(loginUser);
                switch (result)
                {
                    case LoginUserResult.NotFound:
                        TempData[ErrorMessage] = "User Not Found";
                        break;
                    case LoginUserResult.NotActivate:
                        TempData[WarningMessage] = "This User Is Not Active";
                        break;
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByMobile(loginUser.Mobile);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Mobile),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                            new Claim("IsAdmin",user.IsAdmin.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = loginUser.RememberMe
                        };
                        await HttpContext.SignInAsync(principle, properties);
                        TempData[SuccessMessage] = "You are Login Now";
                        return Redirect("/");
                }
            }
            return View(loginUser);
        }
        #endregion

        #region user logout
        [HttpGet("log-Out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            TempData[SuccessMessage] = "Your Account LogOut Now";
            return Redirect("/");
        }
        #endregion
    }
}
