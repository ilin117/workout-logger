using System;

namespace WorkoutLogApp
{
    public class Workout
    {
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }

        public Workout(string exerciseName, int sets, int reps, double weight, DateTime date)
        {
            ExerciseName = exerciseName;
            Sets = sets;
            Reps = reps;
            Weight = weight;
            Date = date;
        }
    }
}
