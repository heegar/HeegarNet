using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heegar.net.Models
{
    public class RSSFeed
    {
        public string PublishDate { get; set; }
        public MvcHtmlString Summary { get; set; }
        public string Title { get; set; }
        public string Link {get;set;}
    }
}