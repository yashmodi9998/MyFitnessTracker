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
            // communicate with workout api to retrieve a list of all workout of users
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

        // GET: Workout/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44324/api/WorkoutData/FindUserWorkout/{id}

            string url = "FindUserWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);


            List<WorkoutDTO> userWorkouts = response.Content.ReadAsAsync<List<WorkoutDTO>>().Result;
            Debug.WriteLine("user workouts received: ");
            Debug.WriteLine(userWorkouts.Count);

            return View(userWorkouts);
        }
        // POST: Workout/Create
        [HttpPost]
        public ActionResult Create(Workout workout)
        {
            Debug.WriteLine("the json payload is :");
            Debug.WriteLine(workout.WorkoutID);
            //objective: add a new workouy into our system using the API
            //curl -H "Content-Type:application/json" -d @workout.json https://localhost:44324/api/workoutdata/addworkout
            string url = "addworkout";

            string jsonpayload = jss.Serialize(workout);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
        public ActionResult Error()
        {

            return View();
        }
        // GET: Workout/New
        public ActionResult New()
        {
            return View();
        }

        // GET: Workout/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the workout information

            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44324/api/animaldata/findanimal/{id}

            string url = "FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);
            WorkoutDTO selectedUSer = response.Content.ReadAsAsync<WorkoutDTO>().Result;
            Debug.WriteLine("Workout received : ");
            //Debug.WriteLine(selectedanimal.AnimalName);

            return View(selectedUSer);
        }

        // POST: Workout/Update/5
        [HttpPost]
        public ActionResult Update(int id, Workout workout)
        {
            try
            {
                Debug.WriteLine("The new Workout info is:");
                Debug.WriteLine(workout.WorkoutID);
                Debug.WriteLine(workout.UserID);

                //serialize into JSON
                //Send the request to the API

                string url = "UpdateWorkout/" + id;


                string jsonpayload = jss.Serialize(workout);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/AnimalData/UpdateAnimal/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;




                return RedirectToAction("Details/" + id);
            }
            catch
            {
                return View();
            }
        }
    }
  
}