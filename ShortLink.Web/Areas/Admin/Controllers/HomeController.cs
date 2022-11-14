using Microsoft.AspNetCore.Mvc;
using ShortLink.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLink.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region constractor
        private readonly ILinkService _linkService;
        public HomeController(ILinkService linkService)
        {
            _linkService = linkService;
        }
        #endregion
        public async Task<IActionResult> Index()
        {
            return View(await _linkService.GetAllLink());
        }
    }
}
