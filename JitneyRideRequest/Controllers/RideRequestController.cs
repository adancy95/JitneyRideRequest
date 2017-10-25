using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JitneyRideRequest.Data;
using JitneyRideRequest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JitneyRideRequest.Models.RideRequestViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JitneyRideRequest.Controllers
{
    [Authorize]
    public class RideRequestController : Controller
    {
        private ApplicationDbContext context;

        public RideRequestController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            // TODO 1: Create Driver's Queue. Get's all the active ride request where activeride = true
            IList<RideRequest> RideQueue = context.RideRequests.Where(r => r.ActiveRequest == true).ToList();

            ViewBag.RideQueue = RideQueue;

            return View();
        }

        public IActionResult NextRider()
        {
            // TODO 2: Maps Rider to the Next Location, and change the color of the Next button
            return RedirectToAction("Index");
        }

        public IActionResult PickedUpRider()
        {
            //Saves the time that the driver picked up the rider.

            //get ride id
            var id = Request.Query["rideID"];

            //find current rider request
            RideRequest currentRide = context.RideRequests.Single(r => r.ID == int.Parse(id));

            currentRide.PickUpTime = DateTime.Now.ToString("h:mm:ss tt");

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DroppingOffRider()
        {
            // TODO 4: Maps driver to the rider's destination
            return RedirectToAction("Index");
        }

        public IActionResult DroppedOff()
        {
            //Saves the time that the driver dropped off the rider

            //get ride id
            var id = Request.Query["rideID"];

            //find current rider request
            RideRequest currentRide = context.RideRequests.Single(r => r.ID == int.Parse(id));

            currentRide.DropOffTime = DateTime.Now.ToString("h:mm:ss tt");

            // Remove Rider from the Queue by making ActiveRide == False
            currentRide.ActiveRequest = false;

            // TODO: Calcuate the Ride Duration using TimeSpan span = endTime.Subtract(StartTime);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult CancelRider()
        {
            var id = Request.Query["rideID"];

            RideRequest currentRide = context.RideRequests.Single(r => r.ID == int.Parse(id));


            //Change status to cancelled
            currentRide.CanceledRide = true;

            //adds the cancellation reason to the database
            currentRide.CancellationReason = "Rider did not show up";

            //Change seats needed to 0 because the driver will not pick up any riders
            currentRide.SeatNeeded = 0;


            //Removes the request from the Ride Request Queue list
            currentRide.ActiveRequest = false;

            //save the changes made to the ride request
            context.SaveChanges();

            return RedirectToAction("Index");
        }




        public IActionResult AddRequest()
        {
            AddRequestViewModel addRequestViewModel = new AddRequestViewModel();

            return View(addRequestViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddRequest(AddRequestViewModel addRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = context.Users.Single(u => u.UserName.Equals(User.Identity.Name));

                RideRequest newRequest = new RideRequest
                {
                    RiderOrgLoc = addRequestViewModel.RiderOrgLoc,
                    RiderDestLoc = addRequestViewModel.RiderDestLoc,
                    SeatNeeded = addRequestViewModel.SeatsNeeded,
                    RequestTime = DateTime.Now.ToString("h:mm:ss tt"),
                    Date = DateTime.Today,
                    UserAccountID = currentUser.Id,
                    ActiveRequest = true





                };

                //Add new ride requesUt to the database
                context.RideRequests.Add(newRequest);
                context.SaveChanges();

                var lastride = context.RideRequests.Last();

                return Redirect("/RideRequest/GetStatus?rideID=" + lastride.ID);
                //return RedirectToAction("GetStatus");
            }
            else
            {
                return View(addRequestViewModel);
            }


        }

        public IActionResult GetStatus(int rideID)
        {
            GetStatusViewModel getStatusViewModel = new GetStatusViewModel();


            ViewBag.rideID = rideID;

            return View(getStatusViewModel);
        }

        [HttpPost]
        public IActionResult CancelRequest(GetStatusViewModel getStatusViewModel)
        {

            var id = Request.Query["rideID"];

            RideRequest currentRide = context.RideRequests.Single(r => r.ID == int.Parse(id));


            //Change status to cancelled
            currentRide.CanceledRide = true;

            //adds the cancellation reason to the database
            currentRide.CancellationReason = getStatusViewModel.CancelRequestViewModel.CancellationReason;

            //context.RideRequests.Update(currentRide);

            // TODO: Remove the request from the Ride Request Queue list
            currentRide.ActiveRequest = false;

            //save the changes made to the ride request
            context.SaveChanges();

            return RedirectToAction("AddRequest");

        }






        [HttpPost]
        public IActionResult EditRequest(GetStatusViewModel getStatusViewModel)
        {


            if (ModelState.IsValid)
            {
                var id = Request.Query["rideID"];

                RideRequest currentRide = context.RideRequests.Single(r => r.ID == int.Parse(id));

                //Change the origin
                currentRide.RiderOrgLoc = getStatusViewModel.EditRequestViewModel.RiderOrgLoc;

                //change the destination
                currentRide.RiderDestLoc = getStatusViewModel.EditRequestViewModel.RiderDestLoc;

                //change the number of seats
                currentRide.SeatNeeded = getStatusViewModel.EditRequestViewModel.SeatsNeeded;

                //context.RideRequests.Update(currentRide);
                //save the changes made to the ride request
                context.SaveChanges();

                // TODO: populate the fields with the information in the database
                // TODO : Add the number of people waiting for a ride in the status


                return RedirectToAction("GetStatus", getStatusViewModel);


            }


            return View("GetStatus", getStatusViewModel);
        }

    }
}
