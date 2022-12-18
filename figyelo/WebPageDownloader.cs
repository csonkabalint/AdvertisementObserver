using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class WebPageDownloader : IAdPageDownloader
    {
        //cookies are still webpage specific (hardverapro.hu)!!! 

        public WebPageDownloader()
        {
        }

        public string GetPage(string url)
        {
            return GetRequest(url);
        }

        public string GetRequest(string url)
        {
            Uri target = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            /*foreach (Cookie c in cookies.GetCookies())
            {
                c.Domain = target.Host;
                request.CookieContainer.Add(c);
            }*/
            request.CookieContainer.Add(new Cookie("prf_ls_uad", "time.d.50.normal") { Domain = target.Host });
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception webEx)
            {
                throw new Exception(webEx.Message);
            }
            
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string rawHtmlString = readStream.ReadToEnd();
            return rawHtmlString;
        }
    }
}
