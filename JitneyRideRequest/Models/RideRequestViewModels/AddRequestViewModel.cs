using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JitneyRideRequest.Models.RideRequestViewModel
{
    public class AddRequestViewModel
    {
        [Required]
        [Display(Name = "Origin")]
        public string RiderOrgLoc { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string RiderDestLoc { get; set; }

        [Required]
        [Display(Name = "Seats Requested?")]
        public int SeatsNeeded { get; set; }


        //Rider Origin Location (latitude, longitude)
        [Required]
        public double RiderOrgLat { get; set; }
        [Required]
        public double RiderOrgLong { get; set; }



        //Rider Destination
        [Required]
        public double RiderDestLat { get; set; }
        [Required]
        public double RiderDestLong { get; set; }

    }
}
