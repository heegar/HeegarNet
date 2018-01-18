using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heegar.net.Models
{
    public class HTMLLinks
    {
        public string Description { get; set; }
        public string Url { get; set; }
    }
    public class FinancialProtectionModel
    {
        public string Title { get; set; }
        public List<HTMLLinks> MoreInformationLinks { get; set; }
        public FinancialProtectionModel()
        {
            Title = "Financial Protection From Living Too Long or Dieing Too Early";

            MoreInformationLinks = new List<HTMLLinks>();

            MoreInformationLinks.Add(new HTMLLinks() { Description = "Dave Ramsey On Financial Protection", Url = "https://www.youtube.com/watch?v=rrBLkfWg_MI" });

            MoreInformationLinks.Add(new HTMLLinks() { Description = "Tips to not outlive your money", Url = "https://www.cnbc.com/2016/08/11/how-to-reduce-the-risk-of-outliving-your-money.html" });

            MoreInformationLinks.Add(new HTMLLinks() { Description = "5 Ways to Avoid Outliving  Your Retirement Savings", Url = "http://time.com/money/4365096/retirement-outlive-savings-avoid/" });

            MoreInformationLinks.Add(new HTMLLinks() { Description = "How to Close the Retirement Income Gap", Url = "https://www.pbs.org/newshour/economy/what-if-you-outlive-your-savin" });

            MoreInformationLinks.Add(new HTMLLinks() { Description = "What to Do if You Run Out of Money in Retirement", Url = "https://www.fool.com/retirement/2017/05/25/what-to-do-if-you-run-out-of-money-in-retirement.aspx" });

            MoreInformationLinks.Add(new HTMLLinks() { Description = "Americans Fear Outliving Their Retirement Savings", Url = "http://www.investopedia.com/articles/personal-finance/101116/americans-fear-outliving-their-retirement-savings.asp" });

            MoreInformationLinks.Add(new HTMLLinks() { Description = "6 Steps To Help Make Sure You Don't Outlive Your Money", Url = "http://www.nasdaq.com/article/6-steps-to-help-make-sure-you-dont-outlive-your-money-cm718485" });
        }
    }
}