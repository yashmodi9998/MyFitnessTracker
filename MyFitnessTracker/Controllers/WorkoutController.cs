using MyFitnessTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace MyFitnessTracker.Controllers
{
    public class WorkoutController : Controller
    {
        // HttpClient to communicate with the Workout API
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static WorkoutController()
        {
            // Initialize the  HttpClient with the base address of the Workout API

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44391/api/WorkoutData/");
        }
        // GET: Workout/List
        public ActionResult List()
        {
            // Fetch a list of workouts from the Workout API
            // https://localhost:44391/api/WorkoutData/ListWorkout
            string url = "ListWorkout";
            HttpResponseMessage response = client.GetAsync(url).Result;
        

            // Convert the response content into a list of WorkoutDTO
            IEnumerable<WorkoutDTO> workouts = response.Content.ReadAsAsync<IEnumerable<WorkoutDTO>>().Result;
          
            // Debug.WriteLine("Number of workouts received : ");
            // Debug.WriteLine(workouts.Count());
            return View(workouts);
        }

        // GET: Workout/Details/5
        public ActionResult Details(int id)
        {

            // https://localhost:44391/api/WorkoutData/FindUserWorkout/{id}
            // Fetch details of a specific workout by ID from the Workout API

            string url = "FindUserWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("find The response code is "+ response.StatusCode);

            // Convert the response content into a list of WorkoutDTO

            //Debug.WriteLine("user workouts received: ");
            //Debug.WriteLine(userWorkouts.Count);
            if (response.IsSuccessStatusCode)
            {
                // Convert the response content into a list of WorkoutDTO
                List<WorkoutDTO> userWorkouts = response.Content.ReadAsAsync<List<WorkoutDTO>>().Result;
                ViewBag.UserId = id;


                    return View(userWorkouts);
              
            }

            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // Read the content directly from the response
                string data = response.Content.ReadAsStringAsync().Result;
                
                Debug.WriteLine("sjshd "+data);
                JObject jsonResponse = JObject.Parse(data);

                // Extract the numeric value from the "Message" property
                string userIdString = jsonResponse["Message"].ToString();
         
                ViewBag.UserId = userIdString;
                return View();
                // Convert the userIdString to an integer
                
           
            }
            else
            {
                // Handle other error cases as needed
                return RedirectToAction("Error");
            }
        }
        // POST: Workout/Create
        [HttpPost]
        public ActionResult Create(int UserId,Workout workout)
        {
            workout.UserID = UserId;
            //curl -H "Content-Type:application/json" -d @workout.json https://localhost:44324/api/workoutdata/addworkout
            // Add a new workout using the Workout API

            string url = "addworkout";
            // Serialize the workout object into JSON

            string jsonpayload = jss.Serialize(workout);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            //To send a POST request to add a new workout

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return Redirect($"~/Workout/Details/{UserId}");
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

        //To create new Workout View
        // GET: Workout/New
        public ActionResult New(int UserID)
        {
            string userInfoUrl = "https://localhost:44391/api/UserData/users"; 
            HttpResponseMessage responses = client.GetAsync(userInfoUrl).Result;
            Debug.WriteLine("New "+responses);


            if (responses.IsSuccessStatusCode)
            {
                List<UserDataDTO> usersInfo = responses.Content.ReadAsAsync<List<UserDataDTO>>().Result;

                // Assuming UserDataDTO has a property named UserID
                int foundUserID = -1; // Default value in case the user is not found

                foreach (UserDataDTO user in usersInfo)
                {
                    if (user.UserId == UserID)
                    {
                        // Found the user, set the foundUserID
                        foundUserID = user.UserId;
                        //break; // Break the loop since we found the user
                    }
                }

                // Now, you can use the foundUserID as needed
                ViewBag.UserID = foundUserID;

                Debug.WriteLine("---- " + foundUserID);
                return View();
            }
            else
            {
                // Handle the case where the request is not successful
                // For example, you can return an error view or display a message
                return RedirectToAction("Error");
            }
            //Debug.WriteLine("Res in new "+responses);
            //List<UserDataDTO> usersInfo = responses.Content.ReadAsAsync<List<UserDataDTO>>().Result;
            //Debug.WriteLine("Users in new " + usersInfo);

            //ViewBag.userID = UserID;

            //Debug.WriteLine("---- " + UserID);*/

            return View();
        }

        // GET: Workout/Edit/5
        public ActionResult Edit(int id)
        {

            //curl https://localhost:44391/api/WorkoutData/FindWorkout/1
            // Fetch details of a specific workout by ID for editing

            string url = "FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Debug.WriteLine("IntoEdit ");
            // Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            // Deserialize the response content into a WorkoutDTO

            WorkoutDTO selectedUSer = response.Content.ReadAsAsync<WorkoutDTO>().Result;
            Debug.WriteLine("Workout received : "+selectedUSer.WorkoutID);

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

                //serialize into JSON
                //Send the request to the API
                // Update the information of an existing workout using the Workout API

                string url = "UpdateWorkout/" + id;

                // Serialize the updated workout object into JSON

                string jsonpayload = jss.Serialize(workout);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

             

                // Send a POST request to update the workout

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                Debug.WriteLine("res : "+response);



                return RedirectToAction("List" );
            }
            catch
            {
                Debug.WriteLine("Error:");

                return View();
            }
        }
        // GET: Workout/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findworkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            WorkoutDTO workout = response.Content.ReadAsAsync<WorkoutDTO>().Result;
            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Delete a workout by ID using the Workout API
            string url = "DeleteWorkout/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            // Send a POST request to delete the workout
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
    }
  
}