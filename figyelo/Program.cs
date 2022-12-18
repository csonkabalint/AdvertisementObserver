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

            INotificationSender emailSender = new EmailNotifier(emailAddress, password, emailAddressTo, emailClientAddress);
            AdObserver observer = new AdObserver(adPageDownloader, advertisementParser, emailSender);
            INotificationSender consoleNotifier = new ConsoleNotifier();
            observer.AddNotificationSender(consoleNotifier);

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

            ISearchObjective GraphicCardNotWorkingSearch = new AdSearchObjective(0, "hardverapro", @"https://hardverapro.hu/aprok/hardver/videokartya/index.html", "faulty graphic cards",
               (str, ar) =>
               ((str.Contains("vega") || str.Contains("fury") || str.Contains("nano")) && ar < 15001)
            || ((str.Contains("1070") || str.Contains("1080")) && ar < 15001)
            || (str.Contains("rtx") && ar < 15001)
            || (str.Contains("rx") && new[] { "5700", "5600", "5500", "6700", "6600", "6500"}.Any(s => str.Contains(s)) && ar < 15001)
            || ((str.Contains("590") || str.Contains("580") || str.Contains("570")) && str.Contains("rx") && ar < 12001)
            || ((str.Contains("480") || str.Contains("470")) && str.Contains("rx") && ar < 10001)
            || (new[] { "1060", "980", "1660", "1650" }.Any(s => str.Contains(s)) && ar < 12001)
            || (new[] { "970", "780", "680", "770" }.Any(s => str.Contains(s)) && ar < 10001)
            || (str.Contains("r9") && new[] { "290", "390" }.Any(s => str.Contains(s)) && ar < 10001)
            || (str.Contains("hd") && new[] { "6970", "7970", "7950" }.Any(s => str.Contains(s)) && ar < 9001)
            || (new[] { "580", "670", "960", "760" }.Any(s => str.Contains(s)) && ar < 7001)
            || (new[] { "lightning", "toxic", "soc", "matrix", "hof", "hall of fame", "classified", "vapor",
                "red devil", "xtreme", "overclocked", "amp", "igame" }.Any(s => str.Contains(s)) && ar < 15001)
            || (str.Contains("kártyák") && ar < 70001)
            || (!str.Contains("hibátlan")
        && new[] { "hibas", "hibá", "csík", "csik", "nem ad", "43", "artif", "rossz", "nem megy",
            "nem müködik", "nincs kép", "nem ismer", "javít", "alkatr", "dísz", "égett", "tönkre",
            "nem indul", "terhelésre", "driver", "fagy", "instabil", "kocká", "hozzáért", "szakért",
            "szervízben", "összeom", "zárlat", "teszteletlen", "pixel", "nem ad kép", "dísz"}.Any(s => str.Contains(s))
        && ar < 31001));


            observer.AddSearchObjective(GraphicCardNotWorkingSearch);
            observer.ObserAds();
        }
    }
}

