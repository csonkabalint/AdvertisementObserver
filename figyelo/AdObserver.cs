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
        List<INotificationSender> notificationSenders;

        public AdObserver(
            IAdPageDownloader pageDownloader,
            IAdPageParser advertisementParser,
            INotificationSender notificationSender)
        {
            searchObjectives = new List<ISearchObjective>();
            notificationSenders = new List<INotificationSender>();
            this.pageDownloader = pageDownloader;
            this.advertisementParser = advertisementParser;
            this.notificationSenders.Add(notificationSender);
        }

        public void AddSearchObjective(ISearchObjective searchObjective)
        {
            searchObjectives.Add(searchObjective);
        }

        public void AddNotificationSender(INotificationSender notificationSender)
        {
            notificationSenders.Add(notificationSender);
        }

        public void ObserAds()
        {
            while (true)
            {

                foreach (var item in searchObjectives)
                {
                    string adPageRawData;
                    try
                    {
                        adPageRawData = pageDownloader.GetPage(item.GetSearchURL());
                    }
                    catch (Exception)
                    {
                        adPageRawData = null;
                    }
                    
                    if(!string.IsNullOrEmpty(adPageRawData))
                    {
                        string selectedAds = advertisementParser.ParseToString(adPageRawData, item);

                        if (!string.IsNullOrEmpty(selectedAds)) 
                        {
                            foreach (var notfSender in notificationSenders)
                            {
                                notfSender.SendNotification(selectedAds, item.GetSearchSubject());
                            }              
                        }
                            
                    }                
                }

                Thread.Sleep(30000);
            }
        }

    }
}
