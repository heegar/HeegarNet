using Heegar.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Heegar.net
{
    public static class HeegarLogic
    {
        public static List<RSSFeed> GetFeedItems(string url, int maxCount = 0)
        {
            try
            {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            HomeModel model = new HomeModel();
            var list = new List<RSSFeed>();
            int itemCount = 0;
            foreach (SyndicationItem item in feed.Items)
            {
                list.Add(new RSSFeed()
                {
                    PublishDate = item.PublishDate.ToString("MMM d yyyy")
                    ,
                    Title = item.Title.Text
                    ,
                    Summary = new MvcHtmlString(item.Summary.Text)
                    ,
                    Link = item.Links[0].Uri.AbsoluteUri
                });

                itemCount++;
                if (maxCount == itemCount)
                    break;
            }

            return list;

            }
            catch (Exception)
            {

                return new List<RSSFeed>();
            }
        }

    }
}