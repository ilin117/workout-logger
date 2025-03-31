using System;
using System.Collections.Generic;
using System.IO;

namespace WorkoutLogApp
{
    public class WorkoutManager
    {
        private List<Workout> workoutLog = new List<Workout>();
        private Dictionary<string, Goal> workoutGoals = new Dictionary<string, Goal>();
        private string workoutLogFilePath = "workout_log.txt";
        private string goalFilePath = "workout_goals.txt";

        public void LogWorkout()
        {
            Console.WriteLine("\nEnter exercise name:");
            string exerciseName = Console.ReadLine();

            Console.WriteLine("Enter number of sets:");
            int sets = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of reps per set:");
            int reps = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter weight lifted (in lbs):");
            double weight = double.Parse(Console.ReadLine());

            Workout workout = new Workout(exerciseName, sets, reps, weight, DateTime.Now);
            workoutLog.Add(workout);
            SaveWorkoutData();

            Console.WriteLine("\nWorkout logged successfully!");
        }

        public void ViewHistory()
        {
            if (workoutLog.Count == 0)
            {
                Console.WriteLine("\nNo workout history found.");
                return;
            }

            Console.WriteLine("\nWorkout History:");
            foreach (var workout in workoutLog)
            {
                Console.WriteLine($"{workout.Date.ToShortDateString()} - {workout.ExerciseName}: {workout.Sets} sets x {workout.Reps} reps @ {workout.Weight}kg");
            }
        }

        public void SetGoal()
        {
            Console.WriteLine("\nEnter the goal type (Weight, Reps, Time):");
            string goalType = Console.ReadLine().ToLower();

            Console.WriteLine("Enter the exercise name for your goal:");
            string exerciseName = Console.ReadLine();

            Console.WriteLine("Enter target value for your goal:");
            double targetValue = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter goal expiration date (YYYY-MM-DD):");
            DateTime expirationDate = DateTime.Parse(Console.ReadLine());

            Goal goal = new Goal(exerciseName, goalType, targetValue, expirationDate);
            workoutGoals[exerciseName] = goal;
            SaveWorkoutGoals();

            Console.WriteLine($"Goal for {exerciseName} set successfully!");
        }

        public void ViewGoal()
        {
            Console.WriteLine("\nEnter the exercise name to view your goal:");
            string exerciseName = Console.ReadLine();

            if (workoutGoals.ContainsKey(exerciseName))
            {
                Goal goal = workoutGoals[exerciseName];
                Console.WriteLine($"Goal for {exerciseName}:");
                Console.WriteLine($"Goal Type: {goal.GoalType}");
                Console.WriteLine($"Target Value: {goal.TargetValue}");
                Console.WriteLine($"Expiration Date: {goal.ExpirationDate.ToShortDateString()}");

                double progress = CalculateGoalProgress(exerciseName);
                double progressPercentage = progress / goal.TargetValue * 100;
                Console.WriteLine($"Progress: {progress} / {goal.TargetValue} ({progressPercentage:F2}%)");
            }
            else
            {
                Console.WriteLine($"No goal set for {exerciseName}.");
            }
        }

        public void ViewAllGoals()
        {
            Console.WriteLine("\nAll Workout Goals:");
            foreach (var goalEntry in workoutGoals)
            {
                var goal = goalEntry.Value;
                Console.WriteLine($"Exercise: {goal.ExerciseName}, Goal Type: {goal.GoalType}, Target: {goal.TargetValue}, Expiry: {goal.ExpirationDate.ToShortDateString()}");
            }
        }

        public void ViewProgress()
        {
            Console.WriteLine("\nEnter the exercise name to track progress:");
            string exerciseName = Console.ReadLine();

            var progress = new List<Workout>();

            foreach (var workout in workoutLog)
            {
                if (workout.ExerciseName.Equals(exerciseName, StringComparison.OrdinalIgnoreCase))
                {
                    progress.Add(workout);
                }
            }

            if (progress.Count == 0)
            {
                Console.WriteLine($"\nNo progress data found for {exerciseName}.");
                return;
            }

            Console.WriteLine($"\nProgress for {exerciseName}:");
            foreach (var workout in progress)
            {
                Console.WriteLine($"{workout.Date.ToShortDateString()} - {workout.Weight}kg");
            }
        }

        public void DeleteWorkout()
        {
            Console.WriteLine("\nEnter the exercise name of the workout you want to delete:");
            string exerciseName = Console.ReadLine();

            Console.WriteLine("Enter the date of the workout to delete (YYYY-MM-DD):");
            DateTime workoutDate = DateTime.Parse(Console.ReadLine());

            Workout workoutToDelete = workoutLog.Find(w => w.ExerciseName.Equals(exerciseName, StringComparison.OrdinalIgnoreCase) && w.Date.Date == workoutDate.Date);

            if (workoutToDelete != null)
            {
                workoutLog.Remove(workoutToDelete);
                SaveWorkoutData();
                Console.WriteLine($"Workout for {exerciseName} on {workoutDate.ToShortDateString()} has been deleted.");
            }
            else
            {
                Console.WriteLine("No workout found for the specified exercise and date.");
            }
        }

        public void Clear()
        {
            if (File.Exists(workoutLogFilePath))
            {
                File.WriteAllText(workoutLogFilePath, string.Empty);
                Console.WriteLine("Workout log file has been cleared.");
            }
            else
            {
                Console.WriteLine("No workout log file found.");
            }

            if (File.Exists(goalFilePath))
            {
                File.WriteAllText(goalFilePath, string.Empty);
                Console.WriteLine("Workout goals file has been cleared.");
            }
            else
            {
                Console.WriteLine("No workout goals file found.");
            }

            workoutLog.Clear();
            workoutGoals.Clear();
        }

        private void SaveWorkoutData()
        {
            using (StreamWriter writer = new StreamWriter(workoutLogFilePath))
            {
                foreach (var workout in workoutLog)
                {
                    writer.WriteLine($"{workout.ExerciseName},{workout.Sets},{workout.Reps},{workout.Weight},{workout.Date}");
                }
            }
        }

        public void LoadWorkoutData()
        {
            if (File.Exists(workoutLogFilePath))
            {
                string[] lines = File.ReadAllLines(workoutLogFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var exerciseName = parts[0];
                    var category = parts[1];
                    var sets = int.Parse(parts[2]);
                    var reps = int.Parse(parts[3]);
                    var weight = double.Parse(parts[4]);
                    var date = DateTime.Parse(parts[5]);

                    workoutLog.Add(new Workout(exerciseName, sets, reps, weight, date));
                }
            }
        }

        private void SaveWorkoutGoals()
        {
            using (StreamWriter writer = new StreamWriter(goalFilePath))
            {
                foreach (var goal in workoutGoals)
                {
                    writer.WriteLine($"{goal.Value.ExerciseName},{goal.Value.GoalType},{goal.Value.TargetValue},{goal.Value.ExpirationDate}");
                }
            }
        }

        public void LoadWorkoutGoals()
        {
            if (File.Exists(goalFilePath))
            {
                string[] lines = File.ReadAllLines(goalFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var exerciseName = parts[0];
                    var goalType = parts[1];
                    var targetValue = double.Parse(parts[2]);
                    var expirationDate = DateTime.Parse(parts[3]);

                    Goal goal = new Goal(exerciseName, goalType, targetValue, expirationDate);
                    workoutGoals[exerciseName] = goal;
                }
            }
        }

        private double CalculateGoalProgress(string exerciseName)
        {
            double totalProgress = 0;

            foreach (var workout in workoutLog)
            {
                if (workout.ExerciseName.Equals(exerciseName, StringComparison.OrdinalIgnoreCase))
                {
                    totalProgress += workout.Weight;
                }
            }

            return totalProgress;
        }
    }
}
