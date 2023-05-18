using System.ComponentModel.DataAnnotations;

namespace ShortLink.Application.DTOs.Link
{
    public class UrlRequestDTO
    {
        [Display(Name = "url اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string OrginalUrl { get; set; }
    }
    public enum UrlRequestResult
    {
        Success,
        Error
    }
}
