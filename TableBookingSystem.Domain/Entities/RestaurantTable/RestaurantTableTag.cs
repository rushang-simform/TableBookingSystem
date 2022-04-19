using TableBookingSystem.Entities.Domain.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Domain.Entities.RestaurantTable
{
    public class RestaurantTableTag : BaseEntity<int>
    {
        public int RestaurantTableTagId { get; set; }
        public int RestaurantTableTagName { get; set; }
        public override int GetId()
        {
            return this.RestaurantTableTagId;
        }
    }
}
