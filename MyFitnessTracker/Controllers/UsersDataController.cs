using MyFitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
                Email = a.Email,
                JoinDate = a.JoinDate
    }));


            // Return the list of UserDTOS
            return UserDTOs;
        }


    }
}
