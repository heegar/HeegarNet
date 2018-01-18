using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heegar.net.Models;

namespace Heegar.net.Controllers
{
    public class ChristianApologeticsController : Controller
    {
        string AskPastorJohnRSS = "http://feed.desiringgod.org/ask-pastor-john.rss";
        string RaviRSS = "https://rzim.org/let-my-people-think-broadcasts/feed/";
        // GET: ChristianAnswers
        public ActionResult Index()
        {
            var model = new ChristianApologeticsModel();

            model.Feed_AskPastorJohn = HeegarLogic.GetFeedItems(AskPastorJohnRSS, 10);
            model.Feed_RaviLetMyPeopleThink = HeegarLogic.GetFeedItems(RaviRSS, 10);
            return View(model);
        }
    }
}