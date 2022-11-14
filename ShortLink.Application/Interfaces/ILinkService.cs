using ShortLink.Application.DTOs.Link;
using ShortLink.Domain.Models.Link;
using ShortLink.Domain.ViewModels.Link;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLink.Application.Interfaces
{
    public interface ILinkService
    {
        #region link
        ShortUrl QuickShortUrl(Uri uri);
        Task<UrlRequestResult> AddLink(ShortUrl url);
        Task AddUserAgent(string userAgnet);
        Task<ShortUrl> FindUrlByToken(string token);
        Task AddRequestUrl(string token);
        Task<List<AllLinkViewModel>> GetAllLink();
        #endregion
    }
}
