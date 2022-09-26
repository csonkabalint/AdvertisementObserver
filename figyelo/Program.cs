using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class Program
    {
        static void Main(string[] args)
        {

            IAdPageDownloader adPageDownloader = new WebPageDownloader();
            IAdAttributeGetter hardveraproAdAttributeGetter = new HardveraproAttributeGetter();
            IAdPageAttributes hardveraproPageAttributes = new HardveraproPageAttributes();
            IAdPageParser advertisementParser = new HtmlAdPageParser();
            advertisementParser.addPageAttribute(hardveraproPageAttributes);
            advertisementParser.addPagerAttributeGetter(hardveraproAdAttributeGetter);

            StreamReader UserData = new StreamReader("..\\..\\..\\user_data.txt");
            string emailAddress = UserData.ReadLine();
            string password = UserData.ReadLine();
            string emailAddressTo = UserData.ReadLine();
            string emailClientAddress = UserData.ReadLine();
            UserData.Close();

            INotificationSender notificationSender = new EmailNotifier(emailAddress, password, emailAddressTo, emailClientAddress);
            AdObserver observer = new AdObserver(adPageDownloader, advertisementParser, notificationSender);

            ISearchObjective FaultyGraphicCardSearch = new AdSearchObjective(0, "hardverapro", @"https://hardverapro.hu/aprok/hardver/videokartya/index.html", "faulty graphic cards",
             (str, ar) =>
                (str.Contains("vega") && ar < 76002)
             || (str.Contains("1070") && str.Contains("ti") && ar < 71002)
             || (str.Contains("1070") && ar < 51002)
             || (str.Contains("1080") && ar < 91002)
             || (str.Contains("fury") && ar < 36002)
             || (str.Contains("rtx") && ar < 91002)
             || (str.Contains("5700") && ar < 91002)
             || (str.Contains("5500") && ar < 51002)
             || (str.Contains("1660") && ar < 71002)
             || (str.Contains("1650") && ar < 36002)
             || (str.Contains("rx") && (str.Contains("590") || str.Contains("580")) && ar < 45002)
             || (str.Contains("1060") && ar < 36002)
             || (str.Contains("980") && ar < 36002)
             || (str.Contains("980") && str.Contains("ti") && ar < 55000)
             || (!str.Contains("hibátlan") && ((str.Contains("hibas") || (str.Contains("csík")) || (str.Contains("hibá")))) && ar < 36002));

            observer.AddSearchObjective(FaultyGraphicCardSearch);
            observer.ObserAds();
        }
    }
}

