using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLink.Application.DTOs.Link;
using ShortLink.Application.Interfaces;
using ShortLink.Domain.Interface;
using ShortLink.Domain.Models.Link;
using ShortLink.Domain.ViewModels.Link;
using ShortLink.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShortLinkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkApiController : ControllerBase
    {
        private ILinkService _linkService;
        private readonly ShortLinkContext _context;
        public LinkApiController(ILinkService linkService, ShortLinkContext context)
        {
            context = _context;
            _linkService = linkService;
        }


        // GET: api/<LinkApiController>
        [HttpGet]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> get(string token)
        {
            var shortUrl = _linkService.FindUrlByToken(token).Result.OrginalUrl.AbsoluteUri;

           return      shortUrl;


        }

        // POST api/<LinkApiController>
        [HttpPost]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Post([FromBody] UrlRequestDTO UrlRequest)
        {
            if (ModelState.IsValid)
            {
                if (UrlRequest.OrginalUrl.Contains("https://") || UrlRequest.OrginalUrl.Contains("http://"))
                {
                    var url = new Uri(UrlRequest.OrginalUrl);
                    var shortUrl = _linkService.QuickShortUrl(url);
                    var result = await _linkService.AddLink(shortUrl);
                    return shortUrl.Value.OriginalString;
                }
            }
            return "BadRequest";
  
        }

        // DELETE api/<LinkApiController>/token

    }
}
