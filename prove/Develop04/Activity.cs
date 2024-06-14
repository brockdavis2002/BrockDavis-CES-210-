using System;

namespace MindfulnessApp
{
    public abstract class Activity
    {
        protected int Duration; // Duration of the activity in seconds

        // Method to display the starting message and set the duration
        public void StartActivity()
        {
            Console.Clear();
            Console.WriteLine($"Starting {GetType().Name} Activity");
            Console.WriteLine(GetDescription());
            Console.Write("Enter the duration of the activity in seconds: ");
            Duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Prepare to begin...");
            Animation.Spinner(3); // Loading animation for 3 seconds
        }

        // Abstract method to get the description of the activity
        protected abstract string GetDescription();

        // Method to display the ending message
        public void EndActivity()
        {
            Console.WriteLine("Good job! You have completed the activity.");
            Console.WriteLine($"You have completed the {GetType().Name} activity for {Duration} seconds.");
            Animation.Spinner(3); // Loading animation for 3 seconds
        }
    }
}
