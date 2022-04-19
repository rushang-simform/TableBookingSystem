using TableBookingSystem.Entities.Domain.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Entities.Domain.RestaurantTable
{
    public class RestaurantTable : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid RestaurantTableId { get; set; }
        public Guid RestaurantId { get; set; }
        public string RestaurantTableName { get; set; }
        public int TotalSeats { get; set; }
        public int TableTypeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public override Guid GetId()
        {
            return this.RestaurantTableId;
        }
    }
}
