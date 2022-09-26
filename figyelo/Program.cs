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
            //jelszora kell valami megoldas
            StreamReader UserData = new StreamReader("..\\..\\..\\user_data.txt");
            string emailAddress = UserData.ReadLine();
            string password = UserData.ReadLine();
            string emailAddressTo = UserData.ReadLine();
            string emailClientAddress = UserData.ReadLine();
            UserData.Close();
            INotificationSender notificationSender = new EmailNotifier(
                emailAddress, password, emailAddressTo, emailClientAddress
                /*"hardverfigyelo@gmail.com", "vmrgvzqwbybhtezg", "hardverfigyelo@gmail.com", "smtp.gmail.com"*/);
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








/*
            Random rnd = new Random();
            Hardverapro videokartya = new Hardverapro(0);
            Hardverapro intel1151 = new Hardverapro(0);
            Hardverapro amdAm4 = new Hardverapro(0);
            Hardverapro intel2011 = new Hardverapro(4489888);
            Hardverapro intelx99 = new Hardverapro(4490985);
            Hardverapro intelx299 = new Hardverapro(4490985);
            Hardverapro intelx299c = new Hardverapro(4490985);

            SendMail mailSender = new SendMail();

            while (true)
            {

                Func<string, int, bool> vidika = (str, ar) =>
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
                || (!str.Contains("hibátlan") && ((str.Contains("hibas") || (str.Contains("csík")) || (str.Contains("hibá")))) && ar < 36002)
                ;

                Func<string, int, bool> intel_1151 = (str, ar) =>
                   (str.Contains("g49") && ar <  10001)
                || (str.Contains("g5") && ar <  12001)
                || (str.Contains("i3")    && ar < 21001)
                || (str.Contains("i5")    && ar < 17100)
                || (str.Contains("i7")    && ar < 21000);

                Func<string, int, bool> amd_am4 = (str, ar) =>
                   (str.Contains("1600") && ar < 21100)
                || (str.Contains("2600") && ar < 21100)
                || (str.Contains("1700") && ar < 21100)
                || (str.Contains("1800") && ar < 21100)
                || (str.Contains("2700") && ar < 21100)
                || (str.Contains("3600") && ar < 36100)
                || (str.Contains("3700") && ar < 46100);

                Func<string, int, bool> intel_2011 = (str, ar) => true;
                Func<string, int, bool> intel_x99 = (str, ar) => true;
                Func<string, int, bool> intel_x299 = (str, ar) => true;
                Func<string, int, bool> intel_x299c = (str, ar) => true;

                // video kartya
                string rawHtmlString_vidika = "";

                try
                {
                     rawHtmlString_vidika = videokartya.GetRequest(@"https://hardverapro.hu/aprok/hardver/videokartya/index.html");
                }
                catch
                {
                    //teljesen helytelen
                    Console.WriteLine("hiba");
                }
                Dictionary<int, string> results_vidika = videokartya.Feldolgoz(rawHtmlString_vidika,90000,vidika);
               

                if (results_vidika.Any())
                {

                    string res = Hardverapro.DicConcat(results_vidika);
                    mailSender.Send(res);
                    
                }


                // intel 1151
                string rawHtmlString_intel1151 = "";

                try
                {
                    rawHtmlString_intel1151 = intel1151.GetRequest(@"https://hardverapro.hu/aprok/hardver/processzor/intel/socket_1151_v2/index.html");
                }
                catch
                {
                    //teljesen helytelen
                    Console.WriteLine("hiba");
                }
                Dictionary<int, string> results_intel1151 = intel1151.Feldolgoz(rawHtmlString_intel1151, 90000, intel_1151);


                if (results_intel1151.Any())
                {

                    string res = Hardverapro.DicConcat(results_intel1151);
                    mailSender.Send(res);
                }

                Thread.Sleep(rnd.Next(15000, 15051));

                // amd am4
                string rawHtmlString_amdAm4 = "";

                try
                {
                    rawHtmlString_amdAm4 = amdAm4.GetRequest(@"https://hardverapro.hu/aprok/hardver/processzor/amd/_socket_am4/index.html");
                }
                catch
                {
                    //teljesen helytelen
                    Console.WriteLine("hiba");
                }
                Dictionary<int, string> results_amdAm4 = amdAm4.Feldolgoz(rawHtmlString_amdAm4, 90000, amd_am4);


                if (results_amdAm4.Any())
                {

                    string res = Hardverapro.DicConcat(results_amdAm4);
                    mailSender.Send(res);
                }

                Thread.Sleep(rnd.Next(15000, 15051));


                // amd intel 2011
                string rawHtmlString_intel2011 = "";

                try
                {
                    rawHtmlString_intel2011 = intel2011.GetRequest(@"https://hardverapro.hu/aprok/hardver/processzor/intel/_socket_2011_v3/index.html");
                }
                catch
                {
                    //teljesen helytelen
                    Console.WriteLine("hiba");
                }
                Dictionary<int, string> results_intel2011 = intel2011.Feldolgoz(rawHtmlString_intel2011, 90000, intel_2011);


                if (results_intel2011.Any())
                {

                    string res = Hardverapro.DicConcat(results_intel2011);
                    mailSender.Send(res);
                }


                Thread.Sleep(rnd.Next(15000, 15051));


                // intel x99
                string rawHtmlString_intelx99 = "";

                try
                {
                    rawHtmlString_intelx99 = intelx99.GetRequest(@"https://hardverapro.hu/aprok/hardver/alaplap/intel/socket_2011_v3/index.html");
                }
                catch
                {
                    //teljesen helytelen
                    Console.WriteLine("hiba");
                }
                Dictionary<int, string> results_intelx99 = intelx99.Feldolgoz(rawHtmlString_intelx99, 90000, intel_x99);


                if (results_intelx99.Any())
                {

                    string res = Hardverapro.DicConcat(results_intelx99);
                    mailSender.Send(res);
                }

                Thread.Sleep(rnd.Next(15000, 15051));


                // intel x299
                string rawHtmlString_intelx299 = "";

                try
                {
                    rawHtmlString_intelx299 = intelx299.GetRequest(@"https://hardverapro.hu/aprok/hardver/alaplap/intel/_socket_2066/index.html");
                }
                catch
                {
                    //teljesen helytelen
                    Console.WriteLine("hiba");
                }
                Dictionary<int, string> results_intelx299 = intelx299.Feldolgoz(rawHtmlString_intelx299, 90000, intel_x299);


                if (results_intelx299.Any())
                {

                    string res = Hardverapro.DicConcat(results_intelx299);
                    mailSender.Send(res);
                }

                Thread.Sleep(rnd.Next(15000, 15051));

                // intel x299c
                string rawHtmlString_intelx299c = "";

                try
                {
                    rawHtmlString_intelx299c = intelx299c.GetRequest(@"https://hardverapro.hu/aprok/hardver/processzor/intel/socket_2066/index.html");
                }
                catch
                {
                    //teljesen helytelen
                    Console.WriteLine("hiba");
                }
                Dictionary<int, string> results_intelx299c = intelx299c.Feldolgoz(rawHtmlString_intelx299c, 90000, intel_x299c);


                if (results_intelx299c.Any())
                {

                    string res = Hardverapro.DicConcat(results_intelx299c);
                    mailSender.Send(res);
                }


                Thread.Sleep(rnd.Next(15000, 15051));
            }

class Hardverapro
{
    int lastID;

    Regex idReg = new Regex("(?<==\")[0-9]{7}(?=\">)");
    Regex arReg = new Regex("(?<=\">)[0-9,\\s]*?(?=Ft</)");
    Regex cimReg = new Regex("(?<=\\.html\">).*?(?=</a> <small)");
    Regex urlReg = new Regex("((?<=href=\")https:).*?(html(?=\">))");


    public Hardverapro(int id)
    {
        lastID = id;
    }

    public string GetRequest(string url)
    {
        Uri target = new Uri(url);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.CookieContainer = new CookieContainer();
        request.CookieContainer.Add(new Cookie("prf_ls_uad", "time.d.50.normal") { Domain = target.Host });
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream receiveStream = response.GetResponseStream();
        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
        string rawHtmlString = readStream.ReadToEnd();
        return rawHtmlString;
    }

    public Dictionary<int, string> Feldolgoz(string rawHtmlString)
    {
        string[] d = rawHtmlString.Split(new[] { "data-uadid" }, StringSplitOptions.None);
        Dictionary<int, string> Results = new Dictionary<int, string>();

        for (int i = 1; i < d.Length - 1; i++)
        {
            int id = int.Parse(idReg.Match(d[i]).Value);

            if (!d[i].Contains("Előresorolt hirdetés") && id > lastID)
            {
                string ar = arReg.Match(d[i]).Value;
                string cim = cimReg.Match(d[i]).Value;
                string url = urlReg.Match(d[i]).Value;
                Results.Add(id, $"ár: {ar.PadLeft(8)} cím: {cim.Substring(0, Math.Min(cim.Length, 30))} url: {url}");
            }
        }

        if (Results.Any())
        {
            lastID = Results.First().Key;
        }

        return Results;
    }

    public Dictionary<int, string> Feldolgoz(string rawHtmlString, int maxar, Func<string, int, bool> f)
    {
        string[] d = rawHtmlString.Split(new[] { "data-uadid" }, StringSplitOptions.None);
        Dictionary<int, string> Results = new Dictionary<int, string>();

        for (int i = 1; i < d.Length - 1; i++)
        {
            int id = int.Parse(idReg.Match(d[i]).Value);
            string ar = arReg.Match(d[i]).Value;
            string cim = cimReg.Match(d[i]).Value;
            string url = urlReg.Match(d[i]).Value;

            if (!d[i].Contains("Előresorolt hirdetés") && ar != "" && id > lastID && (f(cim, int.Parse(Regex.Replace(ar, @"\s+", ""))) || f(url, int.Parse(Regex.Replace(ar, @"\s+", "")))))
            {
                Results.Add(id, $"ár: {ar.PadLeft(8)} cím: {cim.Substring(0, Math.Min(cim.Length, 30))} url: {url}");
            }
        }

        if (Results.Any())
        {
            lastID = Results.First().Key;
        }

        return Results;
    }

    public static string DicConcat(Dictionary<int, string> toConc)
    {
        string res = "";

        foreach (var item in toConc)
        {
            res += $"{item.Key.ToString()}| {item.Value} \n";
        }

        return res;
    }
}


public class SendMail
{
    public void Send(string res)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("hardverfigyelo@gmail.com");
        mail.To.Add("hardverfigyelo@gmail.com");
        mail.Subject = "Test Mail";
        mail.Body = res;

        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential();
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);
        Console.WriteLine("mail Send");
    }
}*/

