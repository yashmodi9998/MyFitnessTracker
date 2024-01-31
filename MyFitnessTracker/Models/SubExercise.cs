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
        [Key]
        public int SubExerciseID { get; set; }

        [ForeignKey("MainExercises")]
       
        public int ExerciseID { get; set; }
        public virtual MainExercise MainExercises { get; set; }

      
        public string SubExerciseName { get; set; }

        public string Description { get; set; }
        
        public ICollection<Workout> workouts { get; set; }
        public ICollection<MainExercise> MainExercisess { get; set; }


    }
}