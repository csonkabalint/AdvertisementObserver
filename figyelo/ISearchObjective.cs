using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    interface ISearchObjective
    {
        Func<string, int, bool> GetSearchFunction();

        string GetSearchURL();

        string GetSearchDomain();

        int GetLastCheckedID();

        void SetLastCheckedID(int lastID);

        string GetSearchSubject();
    }
}
