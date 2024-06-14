using System;
using System.Threading.Tasks;

namespace MindfulnessApp
{
    public class Listing : Activity
    {
        private readonly object lockObj = new object(); // Lock object for thread safety

        // Method to get the description of the listing activity
        protected override string GetDescription()
        {
            return "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        }

        // Method to perform the listing activity
        public void PerformListing()
        {
            string[] prompts = {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt the Holy Ghost this month?",
                "Who are some of your personal heroes?"
            };

            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Length)];
            int secondsLeft = Duration; // Duration of the activity in seconds
            bool timeUp = false; // Flag to check if time is up

            // Start countdown task
            Task.Run(() =>
            {
                Task.Delay(secondsLeft * 1000).Wait(); // Wait for duration
                timeUp = true;
            });

            Console.WriteLine(prompt); // Display the prompt
            Console.WriteLine("Start listing items now...");

            int itemCount = 0;
            while (!timeUp)
            {
                lock (lockObj)
                {
                    Console.SetCursorPosition(0, Console.CursorTop); // Move cursor to beginning of the current line
                    
                }

                Console.Write("Item: ");
                string input = Console.ReadLine(); // User enters an item
                itemCount++;

                // Check if time is up after each input
                if (timeUp)
                    break;
            }

            Console.WriteLine(); // Move to next line for clarity
            Console.WriteLine($"You have listed {itemCount} items.");
        }

        // Override the StartActivity method to include the listing performance
        public new void StartActivity()
        {
            base.StartActivity();
            PerformListing();
            EndActivity(); // Ensure the activity ends with the proper message
        }
    }
}
