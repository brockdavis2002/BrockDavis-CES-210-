using System;

namespace MindfulnessApp
{
    public class Reflecting : Activity
    {
        private string[] prompts = {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private string[] questions = {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        // Method to get the description of the reflecting activity
        protected override string GetDescription()
        {
            return "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        }

        // Method to perform the reflecting activity
        public void PerformReflecting()
        {
            Random random = new Random();

            // Ask for the number of questions
            Console.Write("Enter the number of questions you want to answer: ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfQuestions) || numberOfQuestions <= 0)
            {
                numberOfQuestions = 5; // Default to 5 questions if input is invalid or zero
            }

            int questionDuration = Duration / numberOfQuestions; // Calculate duration per question

            // Display the prompt and wait for the user to be ready
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine($"Consider the following prompt:\n{prompt}");
            Console.WriteLine("When you have something in mind, press Enter to continue... ");
            Animation.WaitForEnterWithAnimation(); // Wait for the user to press Enter with animation

            Console.Clear();
            Console.WriteLine($"Now ponder on each question as they relate to this experience. Duration per question: {questionDuration} seconds.");
            Console.Write("You may begin in: ");
            Animation.Countdown(5); // Countdown from 5 seconds
            Console.Clear();

            for (int i = 0; i < numberOfQuestions; i++)
            {
                string question = questions[random.Next(questions.Length)];
                Console.WriteLine($"{i + 1}. {question}"); // Display question number and text
                Console.WriteLine(); // Blank line for readability

                DateTime questionEndTime = DateTime.Now.AddSeconds(questionDuration);

                while (DateTime.Now < questionEndTime)
                {
                    // Display countdown animation from Animation class
                    Animation.CountdownWithAnimation(questionEndTime.Subtract(DateTime.Now).Seconds);
                }

                Console.Clear(); // Clear the console after each question
            }
        }

        // Override the StartActivity method to include the reflecting performance
        public new void StartActivity()
        {
            base.StartActivity();
            PerformReflecting();
            EndActivity(); // Ensure the activity ends with the proper message
        }
    }
}
