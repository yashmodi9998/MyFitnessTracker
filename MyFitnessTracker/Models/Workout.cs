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

        //this workout table is mainly use for storing details of workout for user.
        //One user can have multiple workouts, but each workout belongs to only one user.So its one to many relationship
        [Key]
        public int WorkoutID { get; set; }
        [ForeignKey("UsersData")]
        public int UserID { get; set; }
        public virtual UserData UsersData { get; set; }

        public DateTime WorkoutDate { get; set; }

   
        public int Duration { get; set; }
        //Many to One: each workout is associated with one main exercise,
        //but a main exercise can be associated with multiple workouts.
        [ForeignKey("MainExercises")]
        public int ExerciseID { get; set; }
        public virtual MainExercise MainExercises { get; set; }

        //Many-to-One:each workout is associated with one sub-exercise,
        //but a sub-exercise can be associated with multiple workouts 
        [ForeignKey("SubExercises")]
        public int SubExerciseID { get; set; }
        public virtual SubExercise SubExercises { get; set; }

    
        public int Weight { get; set; }

    
        public int Reps { get; set; }

        public string Notes { get; set; }

     
    }
    public class WorkoutDTO
    {
        public int WorkoutID { get; set; }
        public int UserID { get; set; }
        public DateTime WorkoutDate { get; set; }
        public string UserName { get; set; }
        public int ExerciseId { get; set; }
        public int SubExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string SubExerciseName { get;set; }
        public int Duration { get; set; }
        public int Weight { get; set; }


        public int Reps { get; set; }

        public string Notes { get; set; }

    }
}