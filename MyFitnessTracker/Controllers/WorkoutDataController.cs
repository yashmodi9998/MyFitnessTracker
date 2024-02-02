using MyFitnessTracker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;

namespace MyFitnessTracker.Controllers
{
    public class WorkoutDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WorkoutData/ListWorkout
        [HttpGet]
        [Route("api/WorkoutData/ListWorkout")]
        public IEnumerable<WorkoutDTO> ListWorkout()
        {
            // Retrieve all workouts from the database
            List<Workout> Workouts = db.Workouts.ToList();

            // Create a list to store WorkoutDTO objects
            List<WorkoutDTO> WorkoutDTOs = new List<WorkoutDTO>();

            // Transform each Workout object into WorkoutDTO and add to the list
            Workouts.ForEach(a => WorkoutDTOs.Add(new WorkoutDTO()
            {
                WorkoutDate = a.WorkoutDate,
                UserName = a.UsersData.FName,
                ExerciseName = a.MainExercises.ExerciseName,
                SubExerciseName = a.SubExercises.SubExerciseName,
                Duration = a.Duration,
                Weight =   a.Weight,
                Reps = a.Reps,
                Notes = a.Notes,
                UserID = a.UserID

                
    }));


            // Return the list of WorkoutDTOs
            return WorkoutDTOs;
        }

        // GET: api/WorkoutData/FindUserWorkout/1
        [ResponseType(typeof(Workout))]
        [HttpGet]
        [Route("api/WorkoutData/FindUserWorkout/{userId}")]
        public IEnumerable<WorkoutDTO> FindUserWorkout(int userId)
        {
            // Retrieve all workouts for the specified user from the database
            // Used Where method to use a query like select * from workout where workout.userId == userId
            List<Workout> userWorkouts = db.Workouts.Where(w => w.UserID == userId).ToList();

            // Create a list to store WorkoutDTO objects
            List<WorkoutDTO> WorkoutDTOs = new List<WorkoutDTO>();

            // Transform each Workout object into WorkoutDTO and add to the list
            userWorkouts.ForEach(a => WorkoutDTOs.Add(new WorkoutDTO()
            {
                WorkoutDate = a.WorkoutDate,
                UserName = a.UsersData.FName,
                ExerciseName = a.MainExercises.ExerciseName,
                SubExerciseName = a.SubExercises.SubExerciseName,
                Duration = a.Duration,
                Weight = a.Weight,
                Reps = a.Reps,
                Notes = a.Notes,
                UserID = a.UserID
            }));
            return WorkoutDTOs;
        }


        // POST: api/WorkoutData/AddWorkout
        [ResponseType(typeof(Workout))]
        [HttpPost]
        [Route("api/WorkoutData/AddWorkout")]
        public IHttpActionResult AddWorkout(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workouts.Add(workout);
            db.SaveChanges();

            return Ok();
            //            curl - H "Content-Type:application/json" - d @workout.json https://localhost:44391/api/WorkoutData/AddWorkout
            //CreatedAtRoute("DefaultApi", new { id = workout.WorkoutID}, workout);
        }



        // POST: api/WorkoutData/DeleteWorkout/1
        [ResponseType(typeof(Workout))]
        [HttpPost]
        [Route("api/WorkoutData/DeleteWorkout/{id}")]
        public IHttpActionResult DeleteWorkout(int id)
        {
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return NotFound();
            }

            db.Workouts.Remove(workout);
            db.SaveChanges();

            return Ok();

//            > curl - d "" https://localhost:44391/api/WorkoutData/DeleteWorkout/2
        }

        // POST: api/WorkoutData/UpdateWorkout/1
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/WorkoutData/UpdateWorkout/{id}")]
        public IHttpActionResult UpdateWorkout(int id, Workout workout)
        {
            Debug.WriteLine("Into the Update Method");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State invalid");
                return BadRequest(ModelState);
            }

            if (id != workout.WorkoutID)
            {
                Debug.WriteLine("Id not matched");
                Debug.WriteLine("Get Param "+id);
                Debug.WriteLine("Post Param"+ workout.WorkoutID);
               // Debug.WriteLine("Post Param"+ workout.UsersData.FName);
                return BadRequest();
            }

            db.Entry(workout).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExist(id))
                {
                    Debug.WriteLine("Workout is not exist");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
            // curl -H "Content-Type:application/json" -d @workoutupdate.json  https://localhost:44391/api/WorkoutData/UpdateWorkout/6
        }

        // Check if a workout with the specified ID exists
        private bool WorkoutExist(int id)
        {
            return db.Workouts.Count(w => w.WorkoutID == id) > 0;
        }
    }
}

