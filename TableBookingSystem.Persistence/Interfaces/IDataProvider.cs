using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Persistence.Interfaces
{
    public interface IDataProvider
    {
        IDbConnection Connection { get; }
    }
}
