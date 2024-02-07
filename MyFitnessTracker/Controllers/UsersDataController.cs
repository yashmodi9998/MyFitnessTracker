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

namespace MyFitnessTracker.Controllers
{
    public class UsersDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserData/Users
        [HttpGet]
        [Route("api/UserData/Users")]
        public IEnumerable<UserDataDTO> ListUser()
        {
            // Retrieve all users from the database
            List<UserData> userDatas = db.UsersData.ToList();

            // Create a list to store UserDataDTO objects
            List<UserDataDTO> UserDTOs = new List<UserDataDTO>();

            // Transform each User object into UserDataDTO and add to the list
            userDatas.ForEach(a => UserDTOs.Add(new UserDataDTO()
            {
                UserId = a.UserId,
                FName = a.FName,
                LName = a.LName,
                PhoneNumber = a.PhoneNumber,
                Email = a.Email,
                JoinDate = a.JoinDate,
                FitnessGoal = a.FitnessGoal
            }));


            // Return the list of UserDTOS
            return UserDTOs;
        }


        // GET: api/UserData/FindUser/1
        [ResponseType(typeof(Workout))]
        [HttpGet]
        [Route("api/UserData/FindUser/{userId}")]
        public IHttpActionResult FindWorkout(int UserId)
        {
            //To find perticulart workoutid
            UserData userData = db.UsersData.Find(UserId);
            //gets all the elements of workout into WorkoutDTO
            UserDataDTO userDataDTO = new UserDataDTO()
            {

                UserId = userData.UserId,
                FName = userData.FName,
                LName = userData.LName,
                PhoneNumber = userData.PhoneNumber,
                Email = userData.Email,
                JoinDate = userData.JoinDate,
                FitnessGoal = userData.FitnessGoal

            };
            //If there's no workout it will return as Not found
            if (userData == null)
            {
                return NotFound();
            }

            return Ok(userDataDTO);

        }
        // POST: api/WorkoutData/AddWorkout
        [ResponseType(typeof(UserData))]
        [HttpPost]
        [Route("api/UserData/AddUser")]
        public IHttpActionResult AddWorkout(UserData userData)
        {
            //Check if modelstate and request is valid or not
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Query that add new workout

            db.UsersData.Add(userData);
            db.SaveChanges();

            return Ok();
            //curl - H "Content-Type:application/json" - d @user.json https://localhost:44391/api/userdata/AddUser
            //CreatedAtRoute("DefaultApi", new { id = user.userId}, userData);
        }



        // POST: api/UserData/DeleteUser/1
        [ResponseType(typeof(UserData))]
        [HttpPost]
        [Route("api/UserData/DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            //Find for the workout that want to delete
            UserData userData= db.UsersData.Find(id);
            //if there is no workout it will return it as notfound;
            if (userData == null)
            {
                return NotFound();
            }
            //Query that removes the workout if it is there
            db.UsersData.Remove(userData);
            db.SaveChanges();

            return Ok();

            //curl - d "" https://localhost:44391/api/UserData/DeleteWorkout/2
        }

        // POST: api/UserData/UpdateUser/1
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/UserData/UpdateUser/{id}")]
        public IHttpActionResult UpdateUser(int id, UserData userData)
        {
            Debug.WriteLine("Into the Update Method");
            //Check if the model state and request is valid or not
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State invalid");
                return BadRequest(ModelState);
            }
            //check if id that want to update is there or not
            //if not it will return it as badRequest
            if (id != userData.UserId)
            {
                Debug.WriteLine("Id not matched");
                Debug.WriteLine("Get Param " + id);
                Debug.WriteLine("Post Param" + userData.UserId);
                return BadRequest();
            }
            //If entry is there it will modify the data
            db.Entry(userData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExist(id))
                {
                    Debug.WriteLine("User is not exist");
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
        private bool UserExist(int id)
        {
            return db.UsersData.Count(u => u.UserId == id) > 0;
        }


    }
}
