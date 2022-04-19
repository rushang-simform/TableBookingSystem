using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableBookingSystem.Entities.Domain.AbstractEntities;

namespace TableBookingSystem.Domain.Entities.Restaurant
{
    public class Restaurant : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public Guid RestaurantCompanyId { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public Decimal? Latitude { get; set; }
        public Decimal? Longitude { get; set; }
        public bool IsDeleted { get; set; }
        public int CurrentStatus { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        public override Guid GetId()
        {
            return this.RestaurantId;
        }
    }
}
