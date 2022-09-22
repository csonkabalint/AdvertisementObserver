using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    class HardveraproPageAttributes : IAdPageAttributes
    {
        public string GetDomainName() { return "hardverapro"; }

        public string[] ForbiddenExpressions()
        {
            return new[] 
            {
                "Előresorolt hirdetés"
            };
        }

        public string SplittingSequence()
        {
            return "data-uadid";
        }
    }
}
