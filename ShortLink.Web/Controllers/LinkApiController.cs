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

namespace ShortLink.Web.Controllers
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
        public async Task<List<AllLinkApiViewModel>> Get()
        {
            return await _linkService.GetAllLinkApi();
        }

        // GET api/<LinkApiController>/5
        [HttpGet("{token}")]
        public async Task<AllLinkApiViewModel> Get([FromRoute]string token)
        { 
            return await _linkService.GetLinkApi(token);
      }

        // POST api/<LinkApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UrlRequestDTO UrlRequest)
        {
            if (ModelState.IsValid)
            {
                if (UrlRequest.OrginalUrl.Contains("https://") || UrlRequest.OrginalUrl.Contains("http://"))
                {
                    var url = new Uri(UrlRequest.OrginalUrl);
                    var shortUrl = _linkService.QuickShortUrl(url);
                    return CreatedAtAction("Get", new { token = shortUrl.Token }, shortUrl);
                }
            } 
            return BadRequest(ModelState);
        }

        // DELETE api/<LinkApiController>/token

    }
}
