﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ShortLink.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortLinkApi.Middelware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ShortLinkUrlRedirect
    {
        private readonly RequestDelegate _next;
        private ILinkService _linkService;

        public ShortLinkUrlRedirect(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _linkService = (ILinkService)httpContext.RequestServices.GetService(typeof(ILinkService));
    

            if(httpContext.Request.Path.ToString().Length == 9 && httpContext.Request.Path.ToString() != "register")
            {
      
                var token = httpContext.Request.Path.ToString().Substring(1);
                var shortUrl = await _linkService.FindUrlByToken(token);
                await _linkService.AddRequestUrl(token);
                if(shortUrl != null)
                {
                    httpContext.Response.Redirect(shortUrl.OrginalUrl.ToString());
                 
                }
                else
                {
                    httpContext.Response.Redirect(httpContext.Request.Host.ToString());
                }
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ShortLinkUrlRedirectExtensions
    {
        public static IApplicationBuilder UseShortLinkUrlRedirect(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShortLinkUrlRedirect>();
        }
    }
}
