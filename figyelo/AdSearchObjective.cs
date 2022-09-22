using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class AdSearchObjective : ISearchObjective
    {
        int lastID;
        string searchDomain;
        string searchURL;
        string searchSubject;
        Func<string, int, bool> searchFunction;

        public AdSearchObjective(int lastID, string searchDomain, string searchURL, string searchSubject, Func<string, int, bool> searchFunction)
        {
            this.lastID = lastID;
            this.searchDomain = searchDomain;
            this.searchURL = searchURL;
            this.searchFunction = searchFunction;
            this.searchSubject = searchSubject;
        }

        public int GetLastCheckedID()
        {
            return lastID;
        }

        public string GetSearchDomain()
        {
            return searchDomain;
        }

        public Func<string, int, bool> GetSearchFunction()
        {
            return searchFunction;
        }

        public string GetSearchSubject()
        {
            return searchSubject;
        }

        public string GetSearchURL()
        {
            return searchURL;
        }

        public void SetLastCheckedID(int lastID)
        {
            this.lastID = lastID;
        }
    }
}
