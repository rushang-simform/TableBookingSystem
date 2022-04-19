using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.DTOs.Restaurant
{
    public class RestaurantDto
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
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int CurrentStatus { get; set; }
    }
}
