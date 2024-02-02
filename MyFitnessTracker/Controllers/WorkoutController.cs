using MyFitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyFitnessTracker.Controllers
{
    public class WorkoutController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static WorkoutController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44391/api/WorkoutData/");
        }
        // GET: Workout/List
        public ActionResult List()
        {
            //1st sem
            //INITIATE bike data controller
            //call list method
            //return list to view

            //            2nd sem
            //objective: communicate with workout api to retrieve a list of all workout of users
            // https://localhost:44324/api/WorkoutData/ListWorkout
            string url = "ListWorkout";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<WorkoutDTO> workouts = response.Content.ReadAsAsync<IEnumerable<WorkoutDTO>>().Result;
            Debug.WriteLine("Number of workouts received : ");
            Debug.WriteLine(workouts.Count());
            return View(workouts);
        }
    }
}