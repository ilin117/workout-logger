using System;
using System.Collections.Generic;

namespace WorkoutLogApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var workoutManager = new WorkoutManager();
            workoutManager.LoadWorkoutData();
            workoutManager.LoadWorkoutGoals();

            Console.WriteLine("Welcome to your Workout Log!");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Log a new workout");
                Console.WriteLine("2. View workout history");
                Console.WriteLine("3. Set a workout goal");
                Console.WriteLine("4. View workout progress");
                Console.WriteLine("5. View workout goal");
                Console.WriteLine("6. View all workout goals");
                Console.WriteLine("7. Clear all data");
                Console.WriteLine("8. Delete a workout");
                Console.WriteLine("9. Exit");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        workoutManager.LogWorkout();
                        break;
                    case "2":
                        workoutManager.ViewHistory();
                        break;
                    case "3":
                        workoutManager.SetGoal();
                        break;
                    case "4":
                        workoutManager.ViewProgress();
                        break;
                    case "5":
                        workoutManager.ViewGoal();
                        break;
                    case "6":
                        workoutManager.ViewAllGoals();
                        break;
                    case "7":
                        workoutManager.Clear();
                        break;
                    case "8":
                        workoutManager.DeleteWorkout();
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
