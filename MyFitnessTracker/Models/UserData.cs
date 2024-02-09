using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFitnessTracker.Models
{
    public class UserData
    {
        //a table that use to store clients details.
        [Key]
        public int UserId { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public string FitnessGoal { get; set; }
        public ICollection<Workout> workouts { get; set; }
    }
    public class UserDataDTO
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FitnessGoal { get; set; }
        public DateTime JoinDate { get; set; }

    }
}