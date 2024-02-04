using MyFitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyFitnessTracker.Controllers
{
    public class ExerciseDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WorkoutData/ListWorkout
        [HttpGet]
        [Route("api/Exercise/list")]
        public IEnumerable<SubExerciseDTO> ListWorkout()
        {
            // Retrieve all workouts from the database
            List<SubExercise> SubExercises = db.SubExercises.ToList();

            // Create a list to store WorkoutDTO objects
            List<SubExerciseDTO> SubExerciseDTOs = new List<SubExerciseDTO>();

            // Transform each Workout object into WorkoutDTO and add to the list
            SubExercises.ForEach(a => SubExerciseDTOs.Add(new SubExerciseDTO()
            {
          
                SubExerciseName = a.SubExerciseName,
                SubExerciseID = a.SubExerciseID,
                ExerciseID  = a.ExerciseID,
                ExerciseName = a.MainExercises.ExerciseName,
          

            }));


            // Return the list of WorkoutDTOs
            return SubExerciseDTOs;
        }

    }
}
