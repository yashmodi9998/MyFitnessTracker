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
| UserFName    | VARCHAR(255) | NOT NULL      |
| UserLName    | VARCHAR(255) | NOT NULL      |
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
# My Fitness Tracker CMS APIs

## Workout APIs

### List All Workouts

- **Endpoint:** `GET /api/WorkoutData/ListWorkout`
- **Description:** API to list out all the workout details.

### Find Workout by ID

- **Endpoint:** `GET /api/WorkoutData/FindWorkout/{id}`
- **Description:** API to get particular workout details with an ID.

### Find User's Workout by ID

- **Endpoint:** `GET /api/WorkoutData/ListUserWorkout/{id}`
- **Description:** API to get a particular user's workout with details using their ID.

### Add New Workout

- **Endpoint:** `POST /api/WorkoutData/AddWorkout`
- **Description:** API for creating a new workout for the user.

### Delete Workout by ID

- **Endpoint:** `POST /api/WorkoutData/DeleteWorkout/{id}`
- **Description:** API for deleting a particular workout with the use of an ID.

### Update User's Workout

- **Endpoint:** `POST /api/WorkoutData/UpdateWorkout/{id}`
- **Description:** API for updating the user's workout.

## User APIs

### List All Users

- **Endpoint:** `GET /api/UserData/Users`
- **Description:** API to list out all user's details.

### Find User by ID

- **Endpoint:** `GET /api/UserData/FindUser/{id}`
- **Description:** API to get particular user details with an ID.

### Add New User

- **Endpoint:** `POST /api/UserData/AddUser`
- **Description:** API for creating a new user registration.

### Delete User by ID

- **Endpoint:** `POST /api/UserData/DeleteUser/{id}`
- **Description:** API for deleting particular users with the use of a user ID.

### Update User Detail

- **Endpoint:** `POST /api/UserData/UpdateUser/{id}`
- **Description:** API for updating the user's detail.


## Exercise APIs

### List Exercise

- **Endpoint:** `GET /api/Exercise/list`
- **Description:** API to list out all exercises.

### Find Exercise by ID

- **Endpoint:** `api/Exercise/FindMainExercise/{id}`
- **Description:** API to get particular Exercise  based on Main ExerciseID.

### Find SubExercise by ID

- **Endpoint:** `api/Exercise/FindSubExercise/{id}`
- **Description:** API to get particular Exercise  based on Sub ExerciseID.

### Find ListWorkoutsForExercise by ID

- **Endpoint:** `api/Exercise/ListWorkoutsForExercise/{id}`
- **Description:** API to get list of workout based on particular Exercise.

### Find ListWorkoutsForSubExercise by ID

- **Endpoint:** `api/Exercise/ListWorkoutsForSubExercise/{id}`
- **Description:** API to get a list of workouts based on a particular Sub Exercise.

### Add New Exercise

- **Endpoint:** `POST /api/Exercise/AddExercise`
- **Description:** API for creating a new Exercise and its sub-exercise.

### Delete MainExercise by ID

- **Endpoint:** `POST /api/Exercise/DeleteMainExercise/{id}`
- **Description:** API for deleting particular exercise using an exerciseID.

### Delete SubExercise by ID

- **Endpoint:** `POST /api/Exercise/DeleteSubExercise/{id}`
- **Description:** API for deleting particular SubExercise using an SubExerciseID.

### Update Sub Exercise by Id

- **Endpoint:** `POST /api/UserData/UpdateSubExercise/{id}`
- **Description:** API for updating the SubExercise.

### Update Main Exercise by Id

- **Endpoint:** `POST /api/UserData/UpdateMainExercise/{id}`
- **Description:** API for updating the mainExercise.





## Workout Views

### List All Workouts

- **Endpoint:** `GET /Workout/List`
- **Description:** To list all user's workouts.

### Display Workout Details

- **Endpoint:** `GET /Workout/Details/{id}`
- **Description:** To display the details of a particular user's workout.

### Create New Workout Form

- **Endpoint:** `GET /Workout/New`
- **Description:** Form for creating new entries of workout.

### Create New Workout Post Request

- **Endpoint:** `POST /Workout/Create`
- **Description:** Post request that creates a new workout.

### Update Workout Form

- **Endpoint:** `GET /Workout/Edit/{id}`
- **Description:** Form for updating the entry of workout.

### Update Workout Post Request

- **Endpoint:** `POST /Workout/Update/{id}`
- **Description:** Post request that updates workout.

### Delete Workout Form

- **Endpoint:** `GET /Workout/Delete/{id}`
- **Description:** Form for the user to delete the workout.

### Delete Workout Post Request

- **Endpoint:** `POST /Workout/Delete/{id}`
- **Description:** Post request that deletes workout.

## User Views

### List All Users

- **Endpoint:** `GET /Users/List`
- **Description:** To list all users.

### Display User Details

- **Endpoint:** `GET /Users/Details/{id}`
- **Description:** To display the details of a particular user's workout.

### Create New User Form

- **Endpoint:** `GET /Users/New`
- **Description:** Form for creating a new entry of the user.

### Create New User Post Request

- **Endpoint:** `POST /Users/Create`
- **Description:** Post request that creates a new user.

### Update User Form

- **Endpoint:** `GET /Users/Edit/{id}`
- **Description:** Form for updating the entry of the user.

### Update User Post Request

- **Endpoint:** `POST /Users/Update/{id}`
- **Description:** Post request that updates user detail.

### Delete User Form

- **Endpoint:** `GET /Users/Delete/{id}`
- **Description:** Form that is used to delete the user.

### Delete User Post Request

- **Endpoint:** `POST /Users/Delete/{id}`
- **Description:** Post request that deletes user data.
