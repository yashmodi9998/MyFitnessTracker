# MyFitnessTracker



## DataController File:
### APIs:
// GET: api/WorkoutData/ListWorkout  
api to list out all the workout details

// GET: api/WorkoutData/FindWorkout/1
api to get perticular workout details with id

// GET: api/WorkoutData/FindUserWorkout/1
api to get perticular user's workout with details

// POST: api/WorkoutData/AddWorkout
api for creating new workout for user
//curl - H "Content-Type:application/json" - d @workout.json https://localhost:44391/api/WorkoutData/AddWorkout
           
// POST: api/WorkoutData/DeleteWorkout/1
api for deleting perticular workout with the use of id
 //curl - d "" https://localhost:44391/api/WorkoutData/DeleteWorkout/2
 
// POST: api/WorkoutData/UpdateWorkout/1
api for updating user's workout
// curl -H "Content-Type:application/json" -d @workoutupdate.json  https://localhost:44391/api/WorkoutData/UpdateWorkout/6


### View:
// GET: Workout/List
To listout all  user's workout
 
 // GET: Workout/Details/5
To display the details of perticular one user's workout

// GET: Workout/New
Form for creating new entry of workout

// POST: Workout/Create
Post request that creates new workout

// GET: Workout/Edit/5
Form for updating entry of workout

// POST: Workout/Update/5
Post request that updates workout

// GET: Workout/Delete/5
form that use to delete the workout

 // POST: Workout/Delete/5
 Post request that deletes workout
