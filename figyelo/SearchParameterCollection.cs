using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class SearchParameterCollection
    {

        static Func<string, int, bool> graphicCard = (str, ar) =>
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
            || (!str.Contains("hibátlan") && ((str.Contains("hibas") || (str.Contains("csík")) || (str.Contains("hibá")))) && ar < 36002);

        static Func<string, int, bool> intel_1151 = (str, ar) =>
               (str.Contains("g49") && ar < 10001)
            || (str.Contains("g5") && ar < 12001)
            || (str.Contains("i3") && ar < 21001)
            || (str.Contains("i5") && ar < 17100)
            || (str.Contains("i7") && ar < 21000);

        static Func<string, int, bool> amd_am4 = (str, ar) =>
               (str.Contains("1600") && ar < 21100)
            || (str.Contains("2600") && ar < 21100)
            || (str.Contains("1700") && ar < 21100)
            || (str.Contains("1800") && ar < 21100)
            || (str.Contains("2700") && ar < 21100)
            || (str.Contains("3600") && ar < 36100)
            || (str.Contains("3700") && ar < 46100);


        Dictionary<string, Tuple<string, int, Func<string, int, bool>>> searchParameters = new Dictionary<string, Tuple<string, int, Func<string, int, bool>>>()
        {
            {@"https://hardverapro.hu/aprok/hardver/videokartya/index.html", Tuple.Create( "hardverapro", 0, graphicCard ) },
            {@"https://hardverapro.hu/aprok/hardver/processzor/intel/socket_1151_v2/index.html", Tuple.Create( "hardverapro", 0, intel_1151)},
            {@"https://hardverapro.hu/aprok/hardver/processzor/amd/_socket_am4/index.html", Tuple.Create( "hardverapro", 0, amd_am4)}
        };

        public Dictionary<string, Tuple<string, int, Func<string, int, bool>>> GetSearchObjectives()
        {
            return searchParameters;
        }
    } 
}
