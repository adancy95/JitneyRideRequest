using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JitneyRideRequest.Models.RideRequestViewModel
{
    public class CancelRequestViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string CancellationReason { get; set; }
    }
}
