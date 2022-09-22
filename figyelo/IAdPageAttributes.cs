using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementObserver
{
    interface IAdPageAttributes
    {
        string GetDomainName();
        string SplittingSequence();
        string[] ForbiddenExpressions();
    }
}
