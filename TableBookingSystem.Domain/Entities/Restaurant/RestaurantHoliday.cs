using TableBookingSystem.Entities.Domain.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Domain.Entities.Restaurant
{
    public class RestaurantHoliday : BaseEntity<int>, IAuditableEntity
    {
        public int RestaurantHolidayId { get; set; }
        public Guid RestaurantId { get; set; }
        public DateTime HolidayDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        public override int GetId()
        {
            return this.RestaurantHolidayId;
        }
    }
}
