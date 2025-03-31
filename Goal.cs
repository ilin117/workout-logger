using System;

namespace WorkoutLogApp
{
    public class Goal
    {
        public string ExerciseName { get; set; }
        public string GoalType { get; set; }
        public double TargetValue { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Goal(string exerciseName, string goalType, double targetValue, DateTime expirationDate)
        {
            ExerciseName = exerciseName;
            GoalType = goalType;
            TargetValue = targetValue;
            ExpirationDate = expirationDate;
        }
    }
}
