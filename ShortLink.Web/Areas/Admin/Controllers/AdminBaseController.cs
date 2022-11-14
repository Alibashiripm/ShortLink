using Microsoft.AspNetCore.Mvc;
namespace ShortLink.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class AdminBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
