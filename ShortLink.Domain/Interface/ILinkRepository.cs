using ShortLink.Domain.Models.Link;
using ShortLink.Domain.ViewModels.Link;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortLink.Domain.Interface
{
    public interface ILinkRepository:IAsyncDisposable
    {
        #region link
        Task AddLink(ShortUrl url);
  
        Task<ShortUrl> FindUrlByToken(string token);
        Task AddRequsetUrl(RequestUrl requestUrl);
        Task<List<AllLinkViewModel>> GetAllLink(); 
        Task<List<AllLinkApiViewModel>> GetAllLinkApi();
        Task<string> GetLinkApi(string token);
     
        #endregion
        Task SaveChange();
    }
}
