# MyFitnessTracker

## Overview

Welcome to My Fitness Tracker CMS, a web application developed using C# with the MVC (Model-View-Controller) architecture. This application is tailored for fitness enthusiasts, offering a centralized platform to track workouts, monitor progress, and set and achieve fitness goals.

## Technologies Used


- **ASP.NET MVC:** The Model-View-Controller architecture is employed for a modular and organized front. C# programming language for server-side logic, offering strong typing and robust features.
- **HTML5 CSS JS:** Standard web technologies are used for the structure and styling of the application.
- **Entity Framework:** An Object-Relational Mapping (ORM) framework for database interactions, providing a simplified data access layer.
- **SQL:** The relational database management system stores and manages data.

## Features

### User Profiles

- Users can create profiles by providing basic information and setting fitness goals.
- The application supports adding, updating, and deleting workout details.

### Workout Logging

- Users/trainers can log workouts, including exercises, sets, reps, duration, weight, and additional notes.
- The application supports adding, updating, and deleting workout details.

### Progress Tracking

- Gym Admins can set and track fitness goals for clients.
- The progress tracking system is designed to keep users motivated and focused on their fitness journey.

## Database Structure

The application utilizes four main tables to store data:

### User Table

| Column      | Data Type    | Constraints   |
|-------------|--------------|---------------|
| UserID      | INT          | PRIMARY KEY   |
| UserName    | VARCHAR(255) | NOT NULL      |
| Email       | VARCHAR(255) | NOT NULL      |
| PhoneNumber | VARCHAR(255) | NOT NULL      |
| JoinDate    | VARCHAR(255) |               |

### Exercise Table

| Column       | Data Type    | Constraints   |
|--------------|--------------|---------------|
| ExerciseID   | INT          | PRIMARY KEY   |
| ExerciseName | VARCHAR(255) | NOT NULL      |
| Description  | TEXT         |               |
| Category     | VARCHAR(50)  |               |

### SubExercise Table

| Column          | Data Type    | Constraints   |
|-----------------|--------------|---------------|
| SubExerciseID   | INT          | PRIMARY KEY   |
| ExerciseID      | INT          | FOREIGN KEY   |
| SubExerciseName | VARCHAR(255) | NOT NULL      |
| Description     | TEXT         |               |

### Workout Table

| Column        | Data Type    | Constraints   |
|---------------|--------------|---------------|
| WorkoutID     | INT          | PRIMARY KEY   |
| UserID        | INT          | FOREIGN KEY   |
| WorkoutDate   | DATE         | NOT NULL      |
| Duration      | INT          | NOT NULL      |
| ExerciseID    | INT          | FOREIGN KEY   |
| SubExerciseID | INT          | FOREIGN KEY   |
| Weight        | INT          | NOT NULL      |
| Reps          | INT          | NOT NULL      |
| Notes         | TEXT         |               |

## APIs

The application exposes two main APIs:

1. **User API:** Manages user-related operations, including user creation, updating profiles, and setting fitness goals.

2. **Workout API:** Handles workout-related operations, such as logging workouts, updating details, and retrieving workout history.

## DataController API:
### Workout:

// GET: api/WorkoutData/ListWorkout 

API to list out all the workout details

// GET: api/WorkoutData/FindWorkout/1

API to get particular workout details with an id

// GET: api/WorkoutData/FindUserWorkout/1

API to get particular user's workout with details

// POST: api/WorkoutData/AddWorkout

API for creating a new workout for the user

//curl - H "Content-Type:application/json" - d @workout.json https://localhost:44391/api/WorkoutData/AddWorkout
           

// POST: api/WorkoutData/DeleteWorkout/1

API for deleting particular workout with the use of id

//curl - d "" https://localhost:44391/api/WorkoutData/DeleteWorkout/2
 

// POST: api/WorkoutData/UpdateWorkout/1

API for updating the user's workout

// curl -H "Content-Type:application/json" -d @workoutupdate.json https://localhost:44391/api/WorkoutData/UpdateWorkout/6


### USERS

// GET: api/UserData/Users

API to list out all the user's details

// GET: api/UserData/FindUser/1

API to get particular user details with id

// POST: api/UserData/AddUser

API for creating a new user registration
           
// POST: api/UserData/DeleteUser/1

API for deleting particular users with the use of user-id

// POST: api/UserData/UpdateUser/1

API for updating the user's detail



## View:

### WORKOUT
// GET: Workout/List

To list all  user's workout
 
// GET: Workout/Details/5

To display the details of particular one user's workout

// GET: Workout/New

Form for creating new entries of workout

// POST: Workout/Create

Post request that creates new workout

// GET: Workout/Edit/5

Form for updating entry of workout

// POST: Workout/Update/5

Post request that updates workout

// GET: Workout/Delete/5

form for user to delete the workout

 // POST: Workout/Delete/5
 
 Post request that deletes workout



### USERS:
// GET: Users/List

To list all  users
 
// GET: Workout/Details/5

To display the details of particular one user's workout

// GET: Users/New

Form for creating a new entry of the user

// POST: Users/Create

Post request that creates a new user

// GET: Users/Edit/5

Form for updating the entry of the user

// POST: Users/Update/5

Post request that updates user detail

// GET: Users/Delete/5

form that used to delete the user

 // POST: Users/Delete/5
 
 Post request that deletes user data
