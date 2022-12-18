using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class ConsoleNotifier : INotificationSender
    {
        public void SendNotification(string notificationMessage, string notificationSubject = "")
        {
            Console.WriteLine(notificationSubject);
            Console.WriteLine(notificationMessage);
        }
    }
}
