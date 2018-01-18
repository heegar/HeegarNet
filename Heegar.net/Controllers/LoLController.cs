using Heegar.LoL.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heegar.net.Controllers
{
    public class LoLController : Controller
    {
        static long KingdomManAccountID = 200569521;
        static long AdamusHeegariusAccountID = 40069102;
        LoLData _lolData;
        private string apiKey = "RGAPI-d46f7453-e306-4e72-b228-81022ece851e";
        // GET: LoL
        public ActionResult Index()
        {
            _lolData = new LoLData(apiKey);

            var profile = _lolData.GetSummonerProfile("KingdomMan");

            return View();
        }
    }
}