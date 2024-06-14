using System;

namespace MindfulnessApp
{
    public abstract class Activity
    {
        public int Duration { get; private set; }

        protected Activity()
        {
            Duration = 300; // Default duration set to 5 minutes (300 seconds)
        }

        protected abstract string GetDescription();

        protected void SetDuration()
        {
            Animation.Spinner(5); // Display spinner for 5 seconds
            Console.Write("Enter duration in seconds (default is 300 seconds or 5 minutes): ");
            if (!int.TryParse(Console.ReadLine(), out int duration) || duration <= 0)
            {
                duration = 300; // Default to 5 minutes if invalid input
            }
            Duration = duration;
        }

        public void StartActivity()
        {
            Console.Clear(); // Clear the console at the start of the activity
            SetDuration(); // Prompt user to set the duration
            Console.WriteLine(GetDescription());
        }

        protected void EndActivity()
        {
            Console.WriteLine("Activity ended. Press any key to continue...");
            Console.ReadKey(true); // Wait for user input before ending
        }

        protected void PerformAskingQuestions(string[] questions)
        {
            Random random = new Random();
            int questionDuration = Duration / questions.Length;

            Console.WriteLine($"Now ponder on each question as they relate to this experience. Duration per question: {questionDuration} seconds.");
            Console.Write("You may begin in: ");
            Animation.Countdown(5); // Countdown from 5 seconds
            Console.Clear();

            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            for (int i = 0; i < questions.Length; i++)
            {
                string question = questions[random.Next(questions.Length)];
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - {question}"); // Display question number and text
                Console.WriteLine(); // Blank line for readability

                System.Threading.Thread.Sleep(questionDuration * 1000); // Sleep for question duration
            }
        }
    }
}
