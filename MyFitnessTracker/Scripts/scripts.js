
async function fetchData() {
    try {
        var response = await fetch('https://localhost:44391/api/Exercise/list');
        var data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}
async function populateExerciseDropdown() {
    var exerciseDropdown = document.getElementById("exerciseDropdown");

    // TO Fetch data from the API
    var data = await fetchData();

    // TO Get unique exercise IDs
    var uniqueExerciseIDs = [...new Set(data.map(item => item.ExerciseID))];

    // TO Populate the Exercise dropdown with unique exercise options
    uniqueExerciseIDs.forEach(function (exerciseID) {
        var exerciseName = data.find(item => item.ExerciseID === exerciseID).ExerciseName;
        var option = document.createElement("option");
        option.value = exerciseID;
        option.text = exerciseName;
        exerciseDropdown.add(option);
    });
}

// Function to update the sub-exercise dropdown based on the selected exercise
function updateSubExerciseDropdown() {
    var exerciseDropdown = document.getElementById("exerciseDropdown");
    var subExerciseDropdown = document.getElementById("subExerciseDropdown");

    // To Clear existing options in the sub-exercise dropdown
    subExerciseDropdown.innerHTML = '<option value="-1">-- Select Sub-Exercise --</option>';

    // To Get the selected exercise ID
    var selectedExerciseID = exerciseDropdown.value;

    // To Fetch data from the API
    fetchData().then(function (data) {
        // Filter sub-exercises based on the selected exercise ID
        var filteredSubExercises = data.filter(function (item) {
            return item.ExerciseID == selectedExerciseID;
        });

        // To Populate the sub-exercise dropdown with filtered options
        filteredSubExercises.forEach(function (subExercise) {
            var option = document.createElement("option");
            option.value = subExercise.SubExerciseID;
            option.text = subExercise.SubExerciseName;
            subExerciseDropdown.add(option);
        });
    });
}

// Call the function to populate the Exercise dropdown on page load
window.onload = function () {
    populateExerciseDropdown();
};
