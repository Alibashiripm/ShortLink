using System.ComponentModel.DataAnnotations;

namespace ShortLink.Application.DTOs.Account
{
    public class EditUserDTO
    {
        public long UserId { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "بلاک شده / نشده")]
        public bool IsBlocked { get; set; }

        [Display(Name = "ادمین هست / نیست")]
        public bool IsAdmin { get; set; }
    }
    public enum EditUserResult
    {
        NotFound,
        Success
    }
}
