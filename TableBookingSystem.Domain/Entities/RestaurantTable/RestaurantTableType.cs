using TableBookingSystem.Entities.Domain.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Domain.Entities.RestaurantTable
{
    public class RestaurantTableType : BaseEntity<int>, IAuditableEntity
    {
        public int RestaurantTableTypeId { get; set; }
        public string RestaurantTableTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public override int GetId()
        {
            return this.RestaurantTableTypeId;
        }
    }
}
