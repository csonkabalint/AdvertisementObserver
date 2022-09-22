using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class AdObserver
    {
        List<ISearchObjective> searchObjectives;
        IAdPageDownloader pageDownloader;
        IAdPageParser advertisementParser;
        INotificationSender notificationSender;

        public AdObserver(
            IAdPageDownloader pageDownloader,
            IAdPageParser advertisementParser,
            INotificationSender notificationSender)
        {
            searchObjectives = new List<ISearchObjective>();
            this.pageDownloader = pageDownloader;
            this.advertisementParser = advertisementParser;
            this.notificationSender = notificationSender;
        }

        public void AddSearchObjective(ISearchObjective searchObjective)
        {
            searchObjectives.Add(searchObjective);
        }


        public void ObserAds()
        {
            while (true)
            {

                foreach (var item in searchObjectives)
                {
                    string adPageRawData = pageDownloader.GetPage(item.GetSearchURL());
                    string selectedAds = advertisementParser.ParseToString(adPageRawData, item);
                    if(!string.IsNullOrEmpty(selectedAds))
                    {
                        notificationSender.SendNotification(selectedAds, item.GetSearchSubject());
                    }                
                }

                Thread.Sleep(30000);
            }
        }

    }
}
