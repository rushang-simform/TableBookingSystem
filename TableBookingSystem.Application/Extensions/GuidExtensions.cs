using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.Extensions
{
    public static class GuidExtensions
    {
        public static string ToCommaSeparatedString(this IList<Guid> guids, bool includeQuotes = false)
        {
            var retVal = string.Join(",", guids.Select(x => includeQuotes ? string.Format("'{0}'", x.ToString()) : x.ToString()));
            return retVal;
        }
    }
}
