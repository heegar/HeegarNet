using Heegar.net.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.Linq;
using Heegar.data;
using Heegar.net;
using System.Net;
using Heegar.LoL.data;

namespace Heegar.net.Controllers
{
    public class HomeController : Controller
    {
        LoLData _lolData;
        private string apiKey = "RGAPI-d46f7453-e306-4e72-b228-81022ece851e";

        //
        // GET: /Home/
        public ActionResult Index()
        {
            //_lolData = new LoLData(apiKey);

            //var profile = _lolData.GetSummonerProfile("KingdomMan");
            //var prof = _lolData.GetSummonerMastery(40069102);

            //ResizeImages();
            string urlBusFin = "http://www.economist.com/sections/business-finance/rss.xml";
            string urlEcon = "http://www.economist.com/sections/economics/rss.xml";
            string urlSciTech = "http://www.economist.com/sections/science-technology/rss.xml";
            string urlUSA = "http://www.economist.com/sections/united-states/rss.xml";
            string urlUpworkASPNetJobs = "https://www.upwork.com/ab/feed/jobs/rss?q=asp.net&api_params=1&securityToken=7129a807182675775a7cff8bd291ed80789424e6c888274f996d48a00377c3910c5ff83f431b4e2fd4d6976acfc25805074d03cb3958cc6f69d9d017851eb89c&userUid=424330617526009856&orgUid=424330617534398465";
            
            HomeModel model = new HomeModel();
            model.Feed_UplinkASPNetJobs = HeegarLogic.GetFeedItems(urlUpworkASPNetJobs, 5);
            model.Feed_Economist_Economics = HeegarLogic.GetFeedItems(urlEcon, 5);
            //model.Feed_Economist_BusinessFinance = HeegarLogic.GetFeedItems(urlBusFin);
            //model.Feed_Economist_ScienceAndTechnology = HeegarLogic.GetFeedItems(urlSciTech);
            //model.Feed_Economist_TheUSA = HeegarLogic.GetFeedItems(urlUSA);
            return View(model);

        }

        private void ResizeImages()
        {
            foreach (string s in Directory.GetFiles(@"C:\Users\heegar\Pictures\2017-03-04"))
            {
                if (s.EndsWith(".JPG") && !s.Contains("ColorWheel"))
                {
                    System.Drawing.Image mg = System.Drawing.Image.FromFile(s);
                    if (mg.Size.Width > 1000)
                    {
                        int width = mg.Size.Width / 4;
                        int height = mg.Size.Height / 4;
                        mg.Dispose();
                        byte[] result;
                        using (System.Drawing.Image thumbnail = new Bitmap(width, height))
                        {
                            using (Bitmap source = new Bitmap(s))
                            {
                                using (Graphics g = Graphics.FromImage(thumbnail))
                                {
                                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    g.DrawImage(source, 0, 0, width, height);
                                }
                            }
                            using (MemoryStream ms = new MemoryStream())
                            {
                                thumbnail.Save(ms, ImageFormat.Jpeg);
                                thumbnail.Save(@"C:\Users\heegar\Pictures\2017-03-04\Resized\" + s.Substring(s.LastIndexOf(@"\") + 1), ImageFormat.Jpeg);
                                result = ms.ToArray();
                            }
                        }
                    }
                }
            }
        }

        private string GetWebsite(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            string result = "Error";
            using (WebResponse myResponse = myRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();
                }
            }
            return result;
        }

        [HttpGet]
        [Route("~/AddCode")]
        public ActionResult AddCode(int item = 0, string loremIpsum = null)
        {
            AddCodeModel model = new AddCodeModel();
            model.SelectedCodeType = item;
            model.SelectedCodeSample = "Coram Deo";
            Heegar.data.MainConnection mc = new data.MainConnection();
            model.CodeTypes = (from ct in mc.CodeTypes
                               select ct).ToList();

            return View(model);
        }


        [HttpPost]
        [Route("~/AddCode")]
        public ActionResult AddCode(AddCodeModel model, string loremIpsum = null)
        {
            if (loremIpsum == "thisistheend")
            {
                if (model.IsAuthenticated)
                {
                    using (var mc = new MainConnection())
                    {

                        var code = model.SelectedCodeSample;
                        var type = model.SelectedCodeType;
                    }
                }
            }
            else
            {
                model.SelectedCodeSample = "sorry, no bueno.";
            }
            return View(model);
        }

        public List<CodeSample> FilterCode(int codeTypeID)
        {
            using (var mc = new MainConnection())
            {
                var cs = (from samples in mc.CodeSamples
                          where samples.CodeTypeID == codeTypeID
                          select samples).ToList();
                return cs;
            }
        }
    }
}