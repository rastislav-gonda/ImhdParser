using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using ImhdParser.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ImhdParser.Service
{
    public class ParseDataService
    {
        HtmlDocument _doc;
        public ParseDataService()
        {
            var html = GetHtml(Configuration.BusStopsUrl);
            _doc = LoadDocument(html);
        }

        private string GetHtml(string uri)
        {
            string html;
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                html = client.DownloadString(uri);
            }
            return html;
        }

        private HtmlDocument LoadDocument(string html)
        {
            HtmlNode.ElementsFlags.Remove("option");
            HtmlNode.ElementsFlags.Remove("form");
            HtmlDocument doc = new HtmlDocument();
            //doc.OptionWriteEmptyNodes = true;
            //doc.OptionFixNestedTags = true;
            //doc.OptionAutoCloseOnEnd = true;
            //doc.OptionCheckSyntax = true;
            doc.OptionOutputAsXml = true;
            doc.LoadHtml(html);
            return doc;
        }

        public List<string> LinesUrl()
        {
            var retVal = new List<string>();
            var test = _doc.DocumentNode.QuerySelectorAll("a[class~=\"linka\"]");
            var busStops = test.Select(a => a.InnerText);
            var links = test.Select(a => a.Attributes["href"].Value);
            return retVal;
        }

        public void GetTimes()
        {

        }

        public List<BusStop> GetBusStops()
        {
            var retVal = new List<BusStop>();
            
            foreach (var form in _doc.DocumentNode.SelectNodes("//form[@name='zastavka']"))
            {
                var options = form.SelectNodes("select/option");
                if (options == null)
                    break;
                foreach (var option in options)
                {
                    retVal.Add(new BusStop()
                    {
                        Id = int.Parse(option.Attributes["value"].Value),
                        Name = option.InnerText
                    });
                }
            }
            return retVal;
        }
    }
}
