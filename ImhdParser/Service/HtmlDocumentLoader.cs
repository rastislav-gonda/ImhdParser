using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImhdParser.Service
{
    public static class HtmlDocumentLoader
    {
        private static WebRequest CreateRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 5000;
            request.UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:36.0) Gecko/20100101 Firefox/36.0";
            return request;
        }
        public static HtmlDocument LoadDocument(string url)
        {
            var document = new HtmlAgilityPack.HtmlDocument();
            try
            {
                using (var responseStream = CreateRequest(url).GetResponse().GetResponseStream())
                {
                    document.Load(responseStream, Encoding.UTF8);
                }
            }
            catch (Exception)
            {
                //just do a second try         
                Thread.Sleep(1000);
                using (var responseStream = CreateRequest(url).GetResponse().GetResponseStream())
                {
                    document.Load(responseStream, Encoding.UTF8);
                }
            }
            return document;
        }
    }
}
