using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class HardveraproAttributeGetter : IAdAttributeGetter
    {
        public string GetDomainName() { return "hardverapro"; }

        Regex idReg = new Regex("(?<==\")[0-9]{7}(?=\">)");
        Regex arReg = new Regex("(?<=\">)[0-9,\\s]*?(?=Ft</)");
        Regex cimReg = new Regex("(?<=\\.html\">).*?(?=</a> <small)");
        Regex urlReg = new Regex("((?<=href=\")https:).*?(html(?=\">))");

        public int GetId(string adContent)
        {
            return int.Parse(idReg.Match(adContent).Value);
        }

        public int? GetPrice(string adContent)
        {
            string priceRaw = arReg.Match(adContent).Value;
            if(priceRaw == "")
            {
                return null;
            }
            else
            {
                return int.Parse(Regex.Replace(priceRaw, @"\s+", ""));
            }
        }

        public string GetTitle(string adContent)
        {
            return cimReg.Match(adContent).Value;
        }

        public string GetUrl(string adContent)
        {
            return urlReg.Match(adContent).Value;
        }
    }
}
