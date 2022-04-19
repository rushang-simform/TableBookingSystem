using TableBookingSystem.Entities.Domain.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Entities.Domain.RestaurantCompany
{
    public class RestaurantCompany : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid RestaurantCompanyId { get; set; }
        public string RestaurantCompanyName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public override Guid GetId()
        {
            return this.RestaurantCompanyId;
        }
    }
}
