using MyFitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

namespace MyFitnessTracker.Controllers
{
    public class ExerciseDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns list of all Exercises and Sub Exercises
        /// </summary>
        /// <returns>
        /// HEADER:200(OK)
        /// </returns>
        /// <example>
        /// GET:https://localhost:44391/api/Exercise/list
        /// response:
        /// <example>
        /// "SubExerciseDTO":<SubExerciseDTO><ExerciseID>1</ExerciseID><ExerciseName>Chest</ExerciseName><SubExerciseID>1</SubExerciseID><SubExerciseName>UpperChest</SubExerciseName></SubExerciseDTO>
        ///<SubExerciseDTO><ExerciseID>1</ExerciseID><ExerciseName>Chest</ExerciseName><SubExerciseID>2</SubExerciseID><SubExerciseName>LowerChest</SubExerciseName></SubExerciseDTO>
        /// </example>
        // GET: api/Exercise/List
        [HttpGet]
        [Route("api/Exercise/list")]
        public IEnumerable<SubExerciseDTO> ListWorkout()
        {
            // Retrieve all exercise from the database
            List<SubExercise> SubExercises = db.SubExercises.ToList();

            // Create a list to store SubExerciseDTO objects
            List<SubExerciseDTO> SubExerciseDTOs = new List<SubExerciseDTO>();

            // Transform each SubExercise object into SubExerciseDTO and add to the list
            SubExercises.ForEach(a => SubExerciseDTOs.Add(new SubExerciseDTO()
            {
          
                SubExerciseName = a.SubExerciseName,
                SubExerciseID = a.SubExerciseID,
                ExerciseID  = a.ExerciseID,
                ExerciseName = a.MainExercises.ExerciseName,
          

            }));
            // Return the list of SubExerciseDTO
            return SubExerciseDTOs;
        }

        /// <summary>
        /// Returns Exercise base on id 
        /// </summary>
        /// <param name="exerciseId">The ID of the user to find its workout.</param>
        /// <returns>
        /// HEADER:200(OK)
        /// </returns>
        /// <example>
        /// GET:api/Exercise/FindMainExercise/1
        /// response:
        /// <example>
        /// <MainExerciseDTO xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/MyFitnessTracker.Models"><Category>Upperbody</Category><Description>chesct des</Description><ExerciseID>1</ExerciseID><ExerciseName>Chest</ExerciseName></MainExerciseDTO>
        ///</example>
       /// GET:api/Exercise/FindMainExercise/1

        [ResponseType(typeof(MainExerciseDTO))]
        [HttpGet]
        [Route("api/Exercise/FindMainExercise/{exerciseId}")]
        public IHttpActionResult FindMainExercise(int exerciseId)
        {
            MainExercise mainExercise= db.MainExercises.Find(exerciseId);
            //gets all the elements of mainExercise into mainExerciseDTO
            MainExerciseDTO mainExerciseDTO = new MainExerciseDTO()
            {

               
                ExerciseID= mainExercise.ExerciseID,
                ExerciseName = mainExercise.ExerciseName,
                
                Category= mainExercise.Category,
                Description = mainExercise.Description,
             

            };
            //If there's no Exercise it will return as Not found
            if (mainExercise == null)
            {
                return NotFound();
            }
            //return mainExerciseDTO
            return Ok(mainExerciseDTO);
        }



        /// <summary>
        /// Returns Exercise base on id 
        /// </summary>
        /// <param name="subExerciseId">The ID of the sub exercise to find its sub exercise .</param>
        /// <returns>
        /// HEADER:200(OK)
        /// </returns>
        /// <example>
        /// GET:api/Exercise/FindSubExercise/1
        /// response:
        /// 200
        /// <example>
        ///<SubExerciseDTO xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/MyFitnessTracker.Models"><Category i:nil="true"/><ExerciseID>1</ExerciseID><ExerciseName>Chest</ExerciseName><MainExerciseDescription i:nil="true"/><SubExerciseDescription>Bar</SubExerciseDescription><SubExerciseID>1</SubExerciseID><SubExerciseName>UpperChest</SubExerciseName></SubExerciseDTO>
        ///</example>
        /// GET:api/Exercise/FindSubExercise/1
        [ResponseType(typeof(SubExerciseDTO))]
        [HttpGet]
        [Route("api/Exercise/FindSubExercise/{subExerciseId}")]
        public IHttpActionResult FindSubExercise(int subExerciseId)
        {
            SubExercise subExercise = db.SubExercises.Find(subExerciseId);
            //gets all the elements of subExercise into subExerciseDTO
            SubExerciseDTO subExerciseDTO = new SubExerciseDTO()
            {


              SubExerciseID = subExercise.SubExerciseID,
              SubExerciseName = subExercise.SubExerciseName,
              ExerciseID = subExercise.ExerciseID,
              SubExerciseDescription = subExercise.Description,
              ExerciseName = subExercise.MainExercises.ExerciseName


            };
            //If there's no subExercise it will return as Not found
            if (subExercise == null)
            {
                return NotFound();
            }
            //return subExerciseDTO
            return Ok(subExerciseDTO);

        }

        /// <summary>
        /// Returns Exercise base on id 
        /// </summary>
        /// <param name="subExerciseId">The ID of the sub exercise to find its Workouts .</param>
        /// <returns>
        /// HEADER:200(OK)
        /// </returns>1
        /// response:
        /// 200
        /// <example>
        ///<WorkoutDTO><Duration>22</Duration><ExerciseId>0</ExerciseId><ExerciseName>Arm</ExerciseName><Notes>Dumble Machine</Notes><Reps>12</Reps><SubExerciseId>0</SubExerciseId><SubExerciseName>Tricep Curl</SubExerciseName>
/       ///<UserID>0</UserID><UserLastName>Vaniyawala</UserLastName><UserName>Milin</UserName><Weight>30</Weight><WorkoutDate>2024-02-07T05:00:00</WorkoutDate><WorkoutID>25</WorkoutID></WorkoutDTO>       ///</example>
         ///</example>
         /// GET :api/Exercise/ListWorkoutsForExercise/
        [ResponseType(typeof(Workout))]
        [HttpGet]
        [Route("api/Exercise/ListWorkoutsForExercise/{exerciseId}")]
        public IHttpActionResult ListWorkoutsForExercise(int exerciseId)
        {
            // Retrieve all workouts for the specified Exercise from the database
        
            List<Workout> userWorkouts = db.Workouts.Where(w => w.ExerciseID == exerciseId).ToList();
   
            List<WorkoutDTO> WorkoutDTOs = new List<WorkoutDTO>();

            // If there are no user workouts, return BadRequest
            if (userWorkouts == null || userWorkouts.Count == 0)
            {
                return BadRequest();
            }

            // Transform each Workout object into WorkoutDTO and add to the list
            userWorkouts.ForEach(a => WorkoutDTOs.Add(new WorkoutDTO()
            {

                WorkoutID = a.WorkoutID,
                WorkoutDate = a.WorkoutDate,
                UserName = a.UsersData.FName,
                UserLastName= a.UsersData.LName,
                ExerciseName = a.MainExercises.ExerciseName,
                SubExerciseName = a.SubExercises.SubExerciseName,
                Duration = a.Duration,
                Weight = a.Weight,
                Reps = a.Reps,
                Notes = a.Notes,
            }));

            return Ok(WorkoutDTOs);
        }
                /// <summary>
        /// Returns Exercise base on id 
        /// </summary>
        /// <param name="subExerciseId">The ID of the  exercise to find its Workouts .</param>
        /// <returns>
        /// HEADER:200(OK)
        /// </returns>1
        /// response:
        /// 200
        /// <example>
        ///<WorkoutDTO><Duration>22</Duration><ExerciseId>1</ExerciseId><ExerciseName>Arm</ExerciseName><Notes>Dumble Machine</Notes><Reps>12</Reps><SubExerciseId>0</SubExerciseId><SubExerciseName>Tricep Curl</SubExerciseName>
/       ///<UserID>0</UserID><UserLastName>Vaniyawala</UserLastName><UserName>Milin</UserName><Weight>30</Weight><WorkoutDate>2024-02-07T05:00:00</WorkoutDate><WorkoutID>25</WorkoutID></WorkoutDTO>       ///</example>
         ///</example>
         /// GET :api/Exercise/ListWorkoutsForSubExercise/4
        [ResponseType(typeof(Workout))]
        [HttpGet]
        [Route("api/Exercise/ListWorkoutsForSubExercise/{subExerciseId}")]
        public IHttpActionResult ListWorkoutsForSubExercise(int subExerciseId)
        {
            // Retrieve all workouts for the specified user from the database
            List<Workout> userWorkouts = db.Workouts.Where(w => w.SubExercises.SubExerciseID == subExerciseId).ToList();
            // list to store WorkoutDTO objects
            List<WorkoutDTO> WorkoutDTOs = new List<WorkoutDTO>();

            // If there are no user workouts, return BadRequest
            if (userWorkouts == null || userWorkouts.Count == 0)
            {
                return BadRequest();
            }

            // Transform each Workout object into WorkoutDTO and add to the list
            userWorkouts.ForEach(a => WorkoutDTOs.Add(new WorkoutDTO()
            {

                WorkoutID = a.WorkoutID,
                WorkoutDate = a.WorkoutDate,
                UserName = a.UsersData.FName,

                UserLastName = a.UsersData.LName,
                ExerciseName = a.MainExercises.ExerciseName,
                SubExerciseName = a.SubExercises.SubExerciseName,
                Duration = a.Duration,
                Weight = a.Weight,
                Reps = a.Reps,
                Notes = a.Notes,
            }));

            return Ok(WorkoutDTOs);
        }

        /// <summary>
        /// Add new Exercise
        /// </summary>
        /// <returns>
        /// HEADER:201(OK)
        /// </returns>
        /// <example>
        /// POST://curl - H "Content-Type:application/json" - d @exercise.json https://localhost:44391/api/Exercise/AddMainExercise
        //CreatedAtRoute("DefaultApi", new { id = mainExercise.ExerciseID}, mainExercise);
        /// response:Ok
        /// </example>



        [ResponseType(typeof(string))]
        [HttpPost]
        [Route("api/Exercise/AddExercise")]
        public IHttpActionResult AddExercise(SubExercise exerciseData)
        {
            try
            {
                // Check if MainExercises is null
                if (exerciseData.MainExercises == null)
                {
                    return BadRequest("MainExercises cannot be null");
                }

                // Create MainExercise object
                MainExercise mainExercise = new MainExercise
                {
                    ExerciseName = exerciseData.MainExercises.ExerciseName,
                    Description = exerciseData.MainExercises.Description,
                    Category = exerciseData.MainExercises.Category
                };
                Debug.WriteLine("Main exewc" + exerciseData.MainExercises.ExerciseName);
                // Add MainExercise to the database
                db.MainExercises.Add(mainExercise);
                db.SaveChanges();

                // Create SubExercise object
                SubExercise subExercise = new SubExercise
                {
                    ExerciseID = mainExercise.ExerciseID,
                    SubExerciseName = exerciseData.SubExerciseName,
                    Description = exerciseData.Description
                };
                Debug.WriteLine("Sub exewc" + exerciseData.SubExerciseName);
                // Add SubExercise to the database
                db.SubExercises.Add(subExercise);
                db.SaveChanges();

                return Ok("Exercise data added successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in AddExercise" + ex.Message );
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// DELETE Main Exercise from Id
        /// </summary>
        /// <returns>
        /// <param name="id">The ID of the Exercise to delete.</param>
        /// HEADER:201(OK)
        /// </returns>
        /// <example>
        /// POST: //curl - d "" https://localhost:44391/api/Exercise/DeleteMainExercise/2
        /// response:Ok
        /// 
        /// </example>

        // POST: api/Exercise/DeleteMainExercise/1
        [ResponseType(typeof(MainExercise))]
        [HttpPost]
        [Route("api/Exercise/DeleteMainExercise/{id}")]
        public IHttpActionResult DeleteMainExercise(int id)
        {

            //Find for the MainExercises that want to delete
            MainExercise mainExercise = db.MainExercises.Find(id);
                Debug.WriteLine("MainExercise " +  mainExercise.ExerciseID);
            //if there is no MainExercises it will return it as notfound;
            if (mainExercise == null)
                {
                    return NotFound();
                }
            try
            {
                //Query that removes the exercise if it is there
                db.MainExercises.Remove(mainExercise);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                // Log or debug the inner exception details
                Debug.WriteLine("Inner Exception: " + innerException.Message);
                throw; 
            }
            return Ok();
        }

        /// <summary>
        /// DELETE SubExercise user with respect to SubExercise id
        /// </summary>
        /// <param name="id">The ID of the SubExercise to delete.</param>
        /// <returns>
        /// HEADER:201(OK)
        /// </returns>
        /// <example>
        /// POST: //curl - d "" https://localhost:44391/api/Exercise/DeleteSubExercise/1
        /// response:Ok
        /// 
        /// </example>

        // POST: api/Exercise/DeleteSubExercise/1
        [ResponseType(typeof(SubExercise))]
        [HttpPost]
        [Route("api/Exercise/DeleteSubExercise/{id}")]
        public IHttpActionResult DeleteSubExercise(int id)
        {
            //Find for the user that want to delete
            SubExercise subExercise = db.SubExercises.Find(id);
            //if there is no user it will return it as notfound;
            if (subExercise == null)
            {
                return NotFound();
            }
            //Query that removes the SubExercise if it is there
            db.SubExercises.Remove(subExercise);
            db.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Update mainexercise with respect to exercise id
        /// </summary>
        /// <param name="id">The ID of the Exercise to update.</param>
        /// <returns>
        /// HEADER:201(OK)
        /// </returns>
        /// <example>
        /// POST:  // curl -H "Content-Type:application/json" -d @exerciseupdate.json  https://localhost:44391/api/Exercise/UpdateMainExercise/1
        /// response:Ok
        /// 
        /// </example>

        // POST: api/Exercise/UpdateMainExercise/1
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/Exercise/UpdateMainExercise/{id}")]
        public IHttpActionResult UpdateMainExercise(int id, MainExercise mainExercise)
        {
            //Debug.WriteLine("Into the Update Method");
            //Check if the model state and request is valid or not
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State invalid");
                return BadRequest(ModelState);
            }
            //check if id that want to update is there or not
            //if not it will return it as badRequest
            if (id != mainExercise.ExerciseID)
            {
                Debug.WriteLine("Id not matched");
                Debug.WriteLine("Get Param " + id);
                Debug.WriteLine("Post Param" + mainExercise.ExerciseID);
                return BadRequest();
            }
            //If entry is there it will modify the data
            db.Entry(mainExercise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainExerciseExist(id))
                {
                    Debug.WriteLine("Exercise is not exist");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
                }
        // Check if a MainExercise with the specified ID exists
        private bool MainExerciseExist(int id)
        {
            return db.MainExercises.Count(m => m.ExerciseID== id) > 0;
        }

        /// <summary>
        /// Update Subexercise with respect to Subexercise id
        /// </summary>
        /// <param name="id">The ID of the Sub Exercise to update.</param>
        /// <returns>
        /// HEADER:201(OK)
        /// </returns>
        /// <example>
        /// POST:  // curl -H "Content-Type:application/json" -d @SubexerciseUpdate.json  https://localhost:44391/api/Exercise/UpdateSubExercise/1
        /// response:Ok
        /// 
        /// </example>

        // POST: api/Exercise/UpdateSubExercise/1
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/Exercise/UpdateSubExercise/{id}")]
        public IHttpActionResult UpdateSubExercise(int id, SubExercise subExercise)
        {
            //Debug.WriteLine("Into the Update Method");
            //Check if the model state and request is valid or not
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State invalid");
                return BadRequest(ModelState);
            }
            //check if id that want to update is there or not
            //if not it will return it as badRequest
            if (id != subExercise.SubExerciseID)
            {
                Debug.WriteLine("Id not matched");
                Debug.WriteLine("Get Param " + id);
                Debug.WriteLine("Post Param" + subExercise.SubExerciseID);
                return BadRequest();
            }
            //If entry is there it will modify the data
            db.Entry(subExercise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubExerciseExist(id))
                {
                    Debug.WriteLine("Exercise is not exist");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        // Check if a SubExercise with the specified ID exists
        private bool SubExerciseExist(int id)
        {
            return db.SubExercises.Count(s => s.SubExerciseID == id) > 0;
        }


    }
}
