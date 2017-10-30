using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JitneyRideRequest.Models.RideRequestViewModels
{
    public class DroppingOffRiderViewModel
    {

        //Rider Destination
        [Required]
        public double RiderDestLat { get; set; }
        [Required]
        public double RiderDestLong { get; set; }

        //Driver Current Location
        [Required]
        public double DriverLat { get; set; }
        [Required]
        public double DriverLong { get; set; }
    }
}
