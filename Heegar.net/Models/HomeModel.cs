using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heegar.net.Models
{
    public class HomeModel
    {
        public List<RSSFeed> Feed_Economist_Economics { get; set; }
        public List<RSSFeed> Feed_Economist_BusinessFinance { get; set; }
        public List<RSSFeed> Feed_Economist_ScienceAndTechnology { get; set; }
        public List<RSSFeed> Feed_Economist_TheUSA { get; set; }
        public List<RSSFeed> Feed_UplinkASPNetJobs { get; set; }
    }
}