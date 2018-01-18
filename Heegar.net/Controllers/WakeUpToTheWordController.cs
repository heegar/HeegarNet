using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Heegar.net.Controllers
{
    public class WakeUpToTheWordController : Controller
    {

        [Route("/WakeUpToTheWord/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("/WakeUpToTheWord/FruitsOfSpirit")]
        public ActionResult FruitsOfSpirit()
        {
            return View();
        }

        [Route("/WakeUpToTheWord/TypesOfSin")]
        public ActionResult TypesOfSin()
        {
            return View();
        }

        [Route("/WakeUpToTheWord/ChristianHedonism")]
        public ActionResult ChristianHedonism()
        {
            return View();
        }

        [Route("/WakeUpToTheWord/WhatIsTheHolySpirit")]
        public ActionResult WhatIsTheHolySpirit()
        {
            return View();
        }

        [Route("/WakeUpToTheWord/CoramDeo")]
        public ActionResult CoramDeo()
        {
            return View();
        }

        [Route("/WakeUpToTheWord/Didache")]
        public ActionResult Didache()
        {
            return View();
        }
    }
}