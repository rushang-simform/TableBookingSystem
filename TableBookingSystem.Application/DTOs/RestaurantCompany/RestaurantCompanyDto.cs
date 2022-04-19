using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.DTOs.RestaurantCompany
{
    public class RestaurantCompanyDto
    {
        public Guid RestaurantCompanyId { get; set; }
        public string RestaurantCompanyName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
    }
}
