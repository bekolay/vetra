using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burak.Models
{
    public class PageModel
    {
        [AllowHtml]
        public string Url { get; set; }
    }
}