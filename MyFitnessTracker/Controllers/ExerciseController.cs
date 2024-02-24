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
    public class ExerciseController : Controller
    {
        // GET: Exercise
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ExerciseController()
        {
            // Initialize the  HttpClient with the base address of the subexercise API

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44391/api/Exercise/");
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            // Fetch a list of Exercise from the ExerciseAPI
            // https://localhost:44391/api/Exercise/ListMainExercise
            string url = "list/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("Ess Ress"+response);
            // Convert the response content into a list of WorkoutDTO
            IEnumerable<WorkoutDTO> workouts = response.Content.ReadAsAsync<IEnumerable<WorkoutDTO>>().Result;

            return View(workouts);
        }
        public ActionResult ListWorkoutByExercise(int id)
        {
            string url = "ListWorkoutsForExercise/" + id;
            Debug.WriteLine("Ess Ress" + url);
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("Ess Ress" + response);
            List<WorkoutDTO> selectedExerciseWorkouts = response.Content.ReadAsAsync<List<WorkoutDTO>>().Result;

            return View(selectedExerciseWorkouts);
        }
        public ActionResult ListWorkoutBySubExercise(int id)
        {
            string url = "ListWorkoutsForSubExercise/" + id;
            Debug.WriteLine("Ess Ress" + url);
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("Ess Ress121" + response);
            List<WorkoutDTO> selectedExerciseWorkouts = response.Content.ReadAsAsync<List<WorkoutDTO>>().Result;

            return View(selectedExerciseWorkouts);
        }
        // POST: Exercise/CreateMainExercise
        [HttpPost]
        public ActionResult CreateMainExercise(SubExercise exerciseData)
        {
            if (exerciseData == null || exerciseData.MainExercises == null)
            {
                // Handle the case where exerciseData or MainExercises is null
                Debug.WriteLine("Execndhfjd null " + exerciseData);
                return RedirectToAction("Error");
            }

            // Add a new user using the user API
            string url = "AddExercise";

            // Serialize the user object into JSON
            string jsonpayload = jss.Serialize(new
            {
                MainExercises = new
                {
                    ExerciseName = exerciseData.MainExercises.ExerciseName,
                    Description = exerciseData.MainExercises.Description,
                    Category = exerciseData.MainExercises.Category
                },
                SubExerciseName = exerciseData.SubExerciseName,
                Description = exerciseData.Description
            });

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine("RESSS " + response);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        // GET: Exercise/New
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: Exercise/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindMainExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);
            MainExerciseDTO mainExerciseDTO = response.Content.ReadAsAsync<MainExerciseDTO>().Result;
            return View(mainExerciseDTO);
        }
        // POST: Exercise/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Delete a Exercise by ID using the Exercise API
            string url = "DeleteMainExercise/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            // Send a POST request to delete the Exercise
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

        // GET: Exercise/DeleteSubExerciseConfirm/5
        public ActionResult DeleteSubExerciseConfirm(int id)
        {
            string url = "FindSubExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("IntoDelete ");
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);
            SubExerciseDTO subExerciseDTO = response.Content.ReadAsAsync<SubExerciseDTO>().Result;
            return View(subExerciseDTO);
        }

        // POST:Exercise/DeleteSubExercise/3
        [HttpPost]
        public ActionResult DeleteSubExercise(int id)
        {
            // Delete a user by ID using the user API
            Debug.WriteLine("Into Delete Confirm ");
            string url = "DeleteSubExercise/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            // Send a POST request to delete the SubExercise
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            Debug.WriteLine("Res: "+response);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Exercise/EditExercise/1
        public ActionResult EditExercise(int id)
        {

            // Fetch details of a specific MainExercise by ID for editing

            string url = "FindMainExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Debug.WriteLine("IntoEdit ");
            // Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            // Deserialize the response content into a MainExerciseDTO

            MainExerciseDTO mainExerciseDTO = response.Content.ReadAsAsync<MainExerciseDTO>().Result;
            Debug.WriteLine("Exercise received : " + mainExerciseDTO.ExerciseID);

            return View(mainExerciseDTO);
        }

        // POST: Exercise/UpdateMainExercise/5
        [HttpPost]
        public ActionResult UpdateMainExercise(int id, MainExercise mainExercise)
        {
            try
            {
                Debug.WriteLine(mainExercise.ExerciseID);

                //serialize into JSON
                //Send the request to the API
                // Update the information of an existing MainExercise using the Exercise API

                string url = "UpdateMainExercise/" + id;

                // Serialize the updated Exercise object into JSON

                string jsonpayload = jss.Serialize(mainExercise);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";



                // Send a POST request to update the Exercise

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


        // GET: Exercise/EditSubExercise/1
        public ActionResult EditSubExercise(int id)
        {

    
            // Fetch details of a specific subexercise by ID for editing

            string url = "FindSubExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

             Debug.WriteLine("IntoEdit ");
             Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            // Deserialize the response content into a subexerciseDTO

            SubExerciseDTO subExerciseDTO = response.Content.ReadAsAsync<SubExerciseDTO>().Result;
            Debug.WriteLine("Exercise received : " + subExerciseDTO.SubExerciseID);

            return View(subExerciseDTO);
        }

        // POST: Exercise/UpdateMainExercise/5
        [HttpPost]
        public ActionResult UpdateSubExercise(int id, SubExercise subExercise)
        {
            try
            {
                Debug.WriteLine(subExercise.SubExerciseID);

                //serialize into JSON
                //Send the request to the API
                // Update the information of an existing subexercise using the subexercise API

                string url = "UpdateSubExercise/" + id;

                // Serialize the updated subexercise object into JSON

                string jsonpayload = jss.Serialize(subExercise);
                Debug.WriteLine("JSON:"+jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";



                // Send a POST request to update the subexercise

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

    }
}