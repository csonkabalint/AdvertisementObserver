using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdvertisementObserver
{
    class HtmlAdPageParser : IAdPageParser
    {
        //int lastID;
        List<IAdAttributeGetter> adAttributeGetters;
        List<IAdPageAttributes> adPageAttributes;

        public HtmlAdPageParser()
        {
            adAttributeGetters = new List<IAdAttributeGetter>();
            adPageAttributes = new List<IAdPageAttributes>();
        }

        public void addPagerAttributeGetter(IAdAttributeGetter adAttributeGetter)
        {
            adAttributeGetters.Add(adAttributeGetter);
        }

        public void addPageAttribute(IAdPageAttributes adPageAttribute)
        {
            adPageAttributes.Add(adPageAttribute);
        }

        public Dictionary<int, string> Parse(string rawHtmlString, ISearchObjective searchObjective)
        {
            return ProcessPage(rawHtmlString, searchObjective);
        }

        public string ParseToString(string rawHtmlString, ISearchObjective searchObjective)
        {
            Dictionary<int, string> ads = ProcessPage(rawHtmlString, searchObjective);

            string res = "";

            foreach (var item in ads)
            {
                res += $"{item.Key.ToString()}| {item.Value} \n";
            }

            return res;
        }

        public Dictionary<int, string> ProcessPage(string rawHtmlString, ISearchObjective searchObjective)
        {
            IAdAttributeGetter adAttributeGetter = adAttributeGetters.FindLast(a => a.GetDomainName() == searchObjective.GetSearchDomain());
            IAdPageAttributes adPageAttribute = adPageAttributes.FindLast(a => a.GetDomainName() == searchObjective.GetSearchDomain());

            string[] d = rawHtmlString.Split(new[] { adPageAttribute.SplittingSequence() }, StringSplitOptions.None);
            Dictionary<int, string> Results = new Dictionary<int, string>();

            for (int i = 1; i < d.Length - 1; i++)
            {
                int id = adAttributeGetter.GetId(d[i]);
                string priceRaw = adAttributeGetter.GetPrice(d[i]);
                string title = adAttributeGetter.GetTitle(d[i]);
                string url = adAttributeGetter.GetUrl(d[i]);

                if (priceRaw != "")
                {
                    int price = int.Parse(Regex.Replace(priceRaw, @"\s+", ""));
                    if (!adPageAttribute.ForbiddenExpressions().Any(p => d[i].Contains(p)) && id > searchObjective.GetLastCheckedID() && (searchObjective.GetSearchFunction()(title, price) || searchObjective.GetSearchFunction()(url, price)))
                    {
                        Results.Add(id, $"ár: {priceRaw.PadLeft(8)} cím: {title.Substring(0, Math.Min(title.Length, 30))} url: {url}");
                    } 
                }
            }

            if (Results.Any())
            {
                searchObjective.SetLastCheckedID(Results.First().Key);
            }

            return Results;
        }
    }
}
