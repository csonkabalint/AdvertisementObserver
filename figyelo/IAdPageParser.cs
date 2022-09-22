using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    interface IAdPageParser
    {
        void addPagerAttributeGetter(IAdAttributeGetter adAttributeGetter);

        void addPageAttribute(IAdPageAttributes adPageAttribute);

        Dictionary<int, string> Parse(string rawHtmlString, ISearchObjective searchObjective);

        string ParseToString(string rawHtmlString, ISearchObjective searchObjective);
    }
}
