using TableBookingSystem.Entities.Domain.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Domain.Entities.User
{
    public class UserRole : BaseEntity<int>
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public override int GetId()
        {
            return this.UserRoleId;
        }
    }
}
