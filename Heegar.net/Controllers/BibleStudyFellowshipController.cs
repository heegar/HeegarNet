using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heegar.net.Controllers
{
    public class BibleStudyFellowshipController : Controller
    {
        //
        // GET: /BibleStudyFellowship/
        public ActionResult Index()
        {
            return View();
        }

        [Route("/BibleStudyFellowship/Acts")]
        public ActionResult Acts()
        {
            return View();
        }

        [Route("/BibleStudyFellowship/Proverbs")]
        public ActionResult Proverbs()
        {
            return View();
        }

        [Route("/BibleStudyFellowship/Galatians")]
        public ActionResult Galatians()
        {
            return View();
        }

        [Route("/BibleStudyFellowship/Ecclesiastes")]
        public ActionResult Ecclesiastes()
        {
            return View();
        }
    }
}