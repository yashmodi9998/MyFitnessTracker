using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFitnessTracker.Models
{
    public class SubExercise
    {
        //this table is use to get data based on mainexercise
        //for eg. if someone select leg as their main exercise it will give all the legs exercise in this table
        [Key]
        public int SubExerciseID { get; set; }

        //Many - one: many sub-exercises can be associated with a single main exercise
        [ForeignKey("MainExercises")]
       
        public int ExerciseID { get; set; }
        public virtual MainExercise MainExercises { get; set; }

      
        public string SubExerciseName { get; set; }

        public string Description { get; set; }
        
        public ICollection<Workout> workouts { get; set; }
        public ICollection<MainExercise> MainExercisess { get; set; }


    }
    public class SubExerciseDTO
    {
        public int ExerciseID { get; set; }
        public string ExerciseName { get; set; }
        public int SubExerciseID { get; set; }
        public string SubExerciseName { get; set; }
    }
}