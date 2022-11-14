using System.ComponentModel.DataAnnotations;

namespace ShortLink.Domain.ViewModels.Account
{
    public class UserForShowViewModel
{
        public long UserId { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "بلاک شده / نشده")]
        public bool IsBlocked { get; set; }

        [Display(Name = "ادمین هست / نیست")]
        public bool IsAdmin { get; set; }
    }
}
