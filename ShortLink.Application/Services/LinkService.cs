using ShortLink.Application.DTOs.Link;
using ShortLink.Application.Generetor;
using ShortLink.Application.Interfaces;
using ShortLink.Domain.Interface;
using ShortLink.Domain.Models.Link;
using ShortLink.Domain.ViewModels.Link;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UAParser;

namespace ShortLink.Application.Services
{
    public class LinkService : ILinkService
    {
        #region constractor
        private readonly ILinkRepository _linkRepository;
        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        
        #endregion

        #region link
        public ShortUrl QuickShortUrl(Uri uri)
        {
            var shortUrl = new ShortUrl();
            shortUrl.OrginalUrl = uri;
            shortUrl.CreateDate = DateTime.Now;
            shortUrl.Token = Generate.Token();
            shortUrl.Value = new Uri($"https://shortlink.bashiridev.ir/{shortUrl.Token}");
            return shortUrl;
        }
        public async Task<UrlRequestResult> AddLink(ShortUrl url)
        {
            if (url == null) return UrlRequestResult.Error;

            await _linkRepository.AddLink(url);
            await _linkRepository.SaveChange();

            return UrlRequestResult.Success;
        }


        public async Task<ShortUrl> FindUrlByToken(string token)
        {
            return await _linkRepository.FindUrlByToken(token);
        }

        public async Task AddRequestUrl(string token)
        {
            var shortUrl = await _linkRepository.FindUrlByToken(token);

            var requestUrl = new RequestUrl
            {
                ShortUrlId = shortUrl.Id,
                RequestDataTime = DateTime.Now
            };

            requestUrl.CreateDate = DateTime.Now;

            await _linkRepository.AddRequsetUrl(requestUrl);
            await _linkRepository.SaveChange();
        }

        public async Task<List<AllLinkViewModel>> GetAllLink()
        {
            return await _linkRepository.GetAllLink();
        }
        public async Task<List<AllLinkApiViewModel>> GetAllLinkApi()
        {
            return await _linkRepository.GetAllLinkApi();
        }   
        public async Task<string> GetLinkApi(string token)
        {
            return await _linkRepository.GetLinkApi(token);
        }


        #endregion
    }
}
