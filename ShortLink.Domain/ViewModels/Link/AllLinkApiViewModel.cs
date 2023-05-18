using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShortLink.Domain.ViewModels.Link
{
    public class AllLinkApiViewModel
    {
        [Display(Name = "url اصلی")]
        public string OrginalUrl { get; set; }

        [Display(Name = "url کامل")]
        public string ShortLink { get; set; }
    }
}
