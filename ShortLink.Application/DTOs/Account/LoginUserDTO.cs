using System.ComponentModel.DataAnnotations;

namespace ShortLink.Application.DTOs.Account
{
    public class LoginUserDTO
    {
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
    public enum LoginUserResult
    {
        NotFound,
        NotActivate,
        Success
    }
}
