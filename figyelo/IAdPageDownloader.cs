using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    interface IAdPageDownloader
    {
        string GetPage(string url);
    }
}
