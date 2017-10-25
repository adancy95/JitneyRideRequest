using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JitneyRideRequest.Models
{
    public class RideRequest
    {
        public int ID { get; set; }

        public bool ActiveRequest { get; set; }


        //Rider Origin Location (latitude, longitude)
        public string RiderOrgLoc { get; set; }
        public double RiderOrgLat { get; set; }
        public double RiderOrgLong { get; set; }



        //Rider Destination
        public string RiderDestLoc { get; set; }
        public double RiderDestLat { get; set; }
        public double RiderDestLong { get; set; }

        //Driver Current Location
        public double DriverLat { get; set; }
        public double DriverLong { get; set; }


        //The number of seats  that a rider requests
        public int SeatNeeded { get; set; }

        //Time Stamp of a Ride Request
        public string RequestTime { get; set; }
        public DateTime Date { get; set; }
        public string PickUpTime { get; set; }
        public string DropOffTime { get; set; }

        //Rider Status
        public string RiderStatus { get; set; }

        //Ride Duration
        public double RideDuration { get; set; }

        //Cancellation
        public bool CanceledRide { get; set; }
        public string CancellationReason { get; set; }

        //Foreign Keys

        public string UserAccountID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string DriverName { get; set; }
    }
}
