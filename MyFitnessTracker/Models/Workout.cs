using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFitnessTracker.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutID { get; set; }
        [ForeignKey("UsersData")]
        public int UserID { get; set; }
        public virtual UserData UsersData { get; set; }

        public DateTime WorkoutDate { get; set; }

   
        public int Duration { get; set; }
       
        [ForeignKey("MainExercises")]
        public int ExerciseID { get; set; }
        public virtual MainExercise MainExercises { get; set; }


        [ForeignKey("SubExercises")]
        public int SubExerciseID { get; set; }
        public virtual SubExercise SubExercises { get; set; }

    
        public int Weight { get; set; }

    
        public int Reps { get; set; }

        public string Notes { get; set; }

     
    }
    public class WorkoutDTO
    {
       // public int WorkoutID { get; set; }
        public DateTime WorkoutDate { get; set; }
        public string UserName { get; set; }
        public string ExerciseName { get; set; }
        public string SubExerciseName { get;set; }

    }
}