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
    public class UsersController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static UsersController()
        {
            // Initialize the  HttpClient with the base address of the User API

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44391/api/UserData/");
        }
        // GET: Users/List
        public ActionResult List()
        {
            // Fetch a list of workouts from the User API
            // https://localhost:44324/api/UserData/User
            string url = "Users";
            HttpResponseMessage response = client.GetAsync(url).Result;
            // Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);


            // Convert the response content into a list of UserDTO
            IEnumerable<UserDataDTO> users = response.Content.ReadAsAsync<IEnumerable<UserDataDTO>>().Result;

            Debug.WriteLine("Number of Users received : ");
             Debug.WriteLine(users.Count());
            return View(users);
        }
        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(UserData userData)
        {

            //curl -H "Content-Type:application/json" -d @workout.json https://localhost:44324/api/UserData/addUser
            // Add a new workout using the Workout API

            string url = "addUser";
            // Serialize the workout object into JSON

            string jsonpayload = jss.Serialize(userData);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            //To send a POST request to add a new user

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
        //To create new Users View
        // GET: Users/New
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Error()
        {

            return View();
        }
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {

            //curl https://localhost:44391/api/UserData/FindUser/1
            // Fetch details of a specific workout by ID for editing

            string url = "FindUser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("IntoEdit ");
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            // Deserialize the response content into a WorkoutDTO

            UserDataDTO userDataDTO = response.Content.ReadAsAsync<UserDataDTO>().Result;        
            return View(userDataDTO);
        }

        // POST: Users/Update/5
        [HttpPost]
        public ActionResult Update(int id, UserData userData)
        {
            try
            {
                Debug.WriteLine("The new Users info is:");
                Debug.WriteLine(userData.UserId);

                //serialize into JSON
                //Send the request to the API
                // Update the information of an existing workout using the User API

                string url = "UpdateUser/" + id;

                // Serialize the updated workout object into JSON

                string jsonpayload = jss.Serialize(userData);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";



                // Send a POST request to update the workout

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                Debug.WriteLine("res : " + response);



                return RedirectToAction("List");
            }
            catch
            {
                Debug.WriteLine("Error:");

                return View();
            }
        }
        // GET: Users/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findUser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("IntoDelete ");
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);
            UserDataDTO users= response.Content.ReadAsAsync<UserDataDTO>().Result;
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Delete a workout by ID using the Workout API
            Debug.WriteLine("Into Delete Confirm ");
            string url = "DeleteUser/" + id;
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