using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Web.Controllers
{
    public class SiteBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
