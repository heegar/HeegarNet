using Heegar.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heegar.net;

namespace Heegar.net.Controllers
{
    public class EconomicNewsController : Controller
    {
        //
        // GET: /EconomicNews/
        public ActionResult Index()
        {
            string urlBusFin = "http://www.economist.com/sections/business-finance/rss.xml";
            string urlEcon = "http://www.economist.com/sections/economics/rss.xml";
            string urlSciTech = "http://www.economist.com/sections/science-technology/rss.xml";
            string urlUSA = "http://www.economist.com/sections/united-states/rss.xml";
            //string urlUpworkASPNetJobs = "https://www.upwork.com/ab/feed/jobs/rss?q=asp.net&api_params=1&securityToken=7129a807182675775a7cff8bd291ed80789424e6c888274f996d48a00377c3910c5ff83f431b4e2fd4d6976acfc25805074d03cb3958cc6f69d9d017851eb89c&userUid=424330617526009856&orgUid=424330617534398465";

            HomeModel model = new HomeModel();
            model.Feed_Economist_Economics = HeegarLogic.GetFeedItems(urlEcon).Take(10).ToList();
            model.Feed_Economist_BusinessFinance = HeegarLogic.GetFeedItems(urlBusFin).Take(10).ToList();
            model.Feed_Economist_ScienceAndTechnology = HeegarLogic.GetFeedItems(urlSciTech).Take(10).ToList();
            model.Feed_Economist_TheUSA = HeegarLogic.GetFeedItems(urlUSA).Take(10).ToList();
            return View(model);
        }
	}
}