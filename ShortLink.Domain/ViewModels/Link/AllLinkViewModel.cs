using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShortLink.Domain.ViewModels.Link
{
    public class AllLinkViewModel
    {
        [Display(Name = "url اصلی")]
        public string OrginalUrl { get; set; }

        [Display(Name = "url کامل")]
        public string Value { get; set; }

        [Display(Name = "توکن")]
        public string Token { get; set; }

        [Display(Name = "تاریخ ایجاد لینک")]
        public DateTime CreateDate { get; set; }
    }
}
