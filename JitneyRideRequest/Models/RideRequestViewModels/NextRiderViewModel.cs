using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JitneyRideRequest.Models.RideRequestViewModels
{
    public class NextRiderViewModel
    {
        //Rider Origin Location (latitude, longitude)
        [Required]
        public double RiderOrgLat { get; set; }
        [Required]
        public double RiderOrgLong { get; set; }

        //Driver Location (latitude, longitude)
        [Required]
        public double DriverLat { get; set; }
        [Required]
        public double DriverLong { get; set; }


    }
}
