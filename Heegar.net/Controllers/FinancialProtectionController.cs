using Heegar.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heegar.net.Controllers
{
    public class FinancialProtectionController : Controller
    {
        // GET: FinancialProtection
        public ActionResult Index()
        {
            var model = new FinancialProtectionModel();
            return View(model);
        }
    }
}