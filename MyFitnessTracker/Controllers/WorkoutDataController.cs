﻿using MyFitnessTracker.Models;
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
                WorkoutID = a.WorkoutID,
                WorkoutDate = a.WorkoutDate,
                UserName = a.UsersData.FName,
                UserLastName = a.UsersData.LName,
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

        // GET: api/WorkoutData/FindWorkout/1
        [ResponseType(typeof(Workout))]
        [HttpGet]
        [Route("api/WorkoutData/FindWorkout/{workoutId}")]
        public IHttpActionResult FindWorkout(int workoutId)
        {
            //To find perticulart workoutid
            Workout workout= db.Workouts.Find(workoutId);
            //gets all the elements of workout into WorkoutDTO
            WorkoutDTO workoutDTOs = new WorkoutDTO()
            {

                WorkoutID = workoutId,
                WorkoutDate = workout.WorkoutDate,
                UserName = workout.UsersData.FName,
                UserLastName = workout.UsersData.LName,
                ExerciseId = workout.ExerciseID,
                SubExerciseId = workout.SubExerciseID,
                ExerciseName = workout.MainExercises.ExerciseName,
                SubExerciseName = workout.SubExercises.SubExerciseName,
                Duration = workout.Duration,
                Weight = workout.Weight,
                Reps = workout.Reps,
                Notes = workout.Notes,
                UserID = workout.UserID

            };
            //If there's no workout it will return as Not found
            if (workout == null)
            {
                return NotFound();
            }
            //return workoutdto
            return Ok(workoutDTOs);

        }

        
        // GET: api/WorkoutData/FindUserWorkout/1
        [ResponseType(typeof(Workout))]
        [HttpGet]
        [Route("api/WorkoutData/FindUserWorkout/{userId}")]
        public IHttpActionResult FindUserWorkout(int userId)
        {
            // Retrieve all workouts for the specified user from the database
            // Used Where method to use a query like select * from workout where workout.userId == userId
            List<Workout> userWorkouts = db.Workouts.Where(w => w.UserID == userId).ToList();
            //Debug.WriteLine("usr wrkout" + userWorkouts.ToList());
            // list to store WorkoutDTO objects
            List<WorkoutDTO> WorkoutDTOs = new List<WorkoutDTO>();


            //if there is no user in workout table. It will get ine user from the users table
            //for adding new workout purpose
            if( userWorkouts == null || userWorkouts.Count == 0)
            {
                UserData user = db.UsersData.Find(userId);
                //it will pass user id from the badrequest.
                return BadRequest(user.UserId.ToString());

            }

            // Transform each Workout object into WorkoutDTO and add to the list
            userWorkouts.ForEach(a => WorkoutDTOs.Add(new WorkoutDTO()
            {
                
            WorkoutID = a.WorkoutID,
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
            //Debug.WriteLine(" wrkout" );
            return Ok(WorkoutDTOs);
        }


       


        // POST: api/WorkoutData/AddWorkout
        [ResponseType(typeof(Workout))]
        [HttpPost]
        [Route("api/WorkoutData/AddWorkout")]
        public IHttpActionResult AddWorkout(Workout workout)
        {
            //Check if modelstate and request is valid or not
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Query that add new workout

            db.Workouts.Add(workout);
            db.SaveChanges();

            return Ok();

            //CURL command to check on command line.
            //curl - H "Content-Type:application/json" - d @workout.json https://localhost:44391/api/WorkoutData/AddWorkout
            //CreatedAtRoute("DefaultApi", new { id = workout.WorkoutID}, workout);
        }



        // POST: api/WorkoutData/DeleteWorkout/1
        [ResponseType(typeof(Workout))]
        [HttpPost]
        [Route("api/WorkoutData/DeleteWorkout/{id}")]
        public IHttpActionResult DeleteWorkout(int id)
        {
            //Find for the workout that want to delete
            Workout workout = db.Workouts.Find(id);
            //if there is no workout it will return it as notfound;
            if (workout == null)
            {
                return NotFound();
            }
            //Query that removes the workout if it is there
            db.Workouts.Remove(workout);
            db.SaveChanges();

            return Ok();

         //curl - d "" https://localhost:44391/api/WorkoutData/DeleteWorkout/2
        }

        // POST: api/WorkoutData/UpdateWorkout/1
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/WorkoutData/UpdateWorkout/{id}")]
        public IHttpActionResult UpdateWorkout(int id, Workout workout)
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
            if (id != workout.WorkoutID)
            {
                Debug.WriteLine("Id not matched");
                Debug.WriteLine("Get Param "+id);
                Debug.WriteLine("Post Param"+ workout.WorkoutID);
               // Debug.WriteLine("Post Param"+ workout.UsersData.FName);
                return BadRequest();
            }
            //If entry is there it will modify the data
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

