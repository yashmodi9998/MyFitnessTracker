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

// curl -H "Content-Type:application/json" -d @workoutupdate.json https://localhost:44391/api/WorkoutData/UpdateWorkout/6


##USERS

// GET: api/UserData/Users

API to list out all the user's details

// GET: api/UserData/FindUser/1

API to get particular user details with id

// POST: api/UserData/AddUser

API for creating new user registeration
           
// POST: api/UserData/DeleteUser/1

API for deleting particular user with the use of userid

// POST: api/UserData/UpdateUser/1

api for updating user's detail



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



##USERS:
### View:
// GET: Users/List

To list all  users
 
// GET: Workout/Details/5

To display the details of perticular one user's workout

// GET: Users/New

Form for creating a new entry of the user

// POST: Users/Create

Post request that creates a new user

// GET: Users/Edit/5

Form for updating the entry of user

// POST: Users/Update/5

Post request that updates user detail

// GET: Workout/Delete/5

form that use to delete the workout

 // POST: Workout/Delete/5
 
 Post request that deletes workout
