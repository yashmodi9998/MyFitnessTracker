using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFitnessTracker.Models
{
    public class MainExercise
    {
        [Key]
        public int ExerciseID { get; set; }

       
        public string ExerciseName { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        // Navigation property for the one-to-many relationship with WorkoutModel
        public ICollection<Workout> Workouts { get; set; }
        public ICollection<SubExercise> SubExercises{ get; set; }
    }
}
