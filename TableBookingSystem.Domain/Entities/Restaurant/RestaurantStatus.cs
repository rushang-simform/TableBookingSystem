using TableBookingSystem.Entities.Domain.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Domain.Entities.Restaurant
{
    public class RestaurantStatus : BaseEntity<int>
    {
        public int RestaurantStatusId { get; set; }
        public string RestaurantStatusName { get; set; }

        public override int GetId()
        {
            return this.RestaurantStatusId;
        }
    }
}
