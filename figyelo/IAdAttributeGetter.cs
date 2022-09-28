using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    interface IAdAttributeGetter
    {
        string GetDomainName();
        int GetId(string adContent);
        int? GetPrice(string adContent);
        string GetTitle(string adContent);
        string GetUrl(string adContent);
    }
}
