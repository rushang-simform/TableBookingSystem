using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Entities.Domain.AbstractEntities
{
    public abstract class BaseEntity<T>
    {
        public abstract T GetId();
    }
}
