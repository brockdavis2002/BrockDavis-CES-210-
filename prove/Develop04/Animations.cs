using System;
using System.Threading;

namespace MindfulnessApp
{
    public static class Animation
    {
        // Method to show a spinner animation for a given duration in seconds
        public static void Spinner(int seconds)
        {
            int counter = 0;
            while (seconds > 0)
            {
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Thread.Sleep(250); // Pause for 250 milliseconds
                Console.Write("\b \b"); // Erase the previous character
                counter++;
                if (counter % 4 == 0) seconds--; // Reduce the seconds counter every full rotation
            }
        }

        // Method to show a countdown animation for a given duration in seconds
        public static void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                // Adding dots
                for (int j = 0; j < 5; j++) // Fixed number of dots for demonstration
                {
                    Console.Write(".");
                    Thread.Sleep(200); // Pause for 0.2 seconds
                }

                // Removing dots
                for (int j = 5; j > 0; j--)
                {
                    Console.Write("\b \b"); // Erase dot
                    Thread.Sleep(200); // Pause for 0.2 seconds
                }

                // Countdown number display
                Console.Write($"{i} "); // Display the countdown number
                Thread.Sleep(800); // Pause for 0.8 seconds before next cycle
                if (i > 9)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop); // Move cursor back to overwrite number
                }
                else
                {
                    Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop); // Move cursor back to overwrite number
                }
            }
            Console.WriteLine(); // Move to the next line after countdown finishes
        }

        // Method to show a countdown animation with cycling dots and countdown display
        public static void CountdownWithAnimation(int seconds)
        {
            int totalDots = 5; // Maximum number of dots

            for (int i = seconds; i > 0; i--)
            {
                // Adding dots
                for (int j = 0; j < totalDots; j++)
                {
                    Console.Write(".");
                    Thread.Sleep(200); // Pause for 0.2 seconds
                }

                // Removing dots
                for (int j = totalDots; j > 0; j--)
                {
                    Console.Write("\b \b"); // Erase dot
                    Thread.Sleep(200); // Pause for 0.2 seconds
                }

                // Countdown number display
                Console.Write($"{i} "); // Display the countdown number
                Thread.Sleep(800); // Pause for 0.8 seconds before next cycle
                if (i > 9)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop); // Move cursor back to overwrite number
                }
                else
                {
                    Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop); // Move cursor back to overwrite number
                }
            }
            Console.WriteLine(); // Move to the next line after countdown finishes
        }

        // Method to show an animation while waiting for the user to press Enter
        public static void WaitForEnterWithAnimation()
        {
            int totalDots = 5; // Maximum number of dots

            while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                // Adding dots
                for (int j = 0; j < totalDots; j++)
                {
                    Console.Write(".");
                    Thread.Sleep(200); // Pause for 0.2 seconds
                }

                // Removing dots
                for (int j = totalDots; j > 0; j--)
                {
                    Console.Write("\b \b"); // Erase dot
                    Thread.Sleep(200); // Pause for 0.2 seconds
                }
            }
            Console.WriteLine(); // Move to the next line after animation
        }

        // Method to show a custom animation for breathing activity
        public static void BreathingAnimation(int breatheInSeconds, int breatheOutSeconds, int duration)
        {
            DateTime endTime = DateTime.Now.AddSeconds(duration);
            while (DateTime.Now < endTime)
            {
                Console.WriteLine("Breathe in... ");
                Countdown(breatheInSeconds); // Countdown for breathing in
                Console.WriteLine();
                Console.WriteLine("Breathe out... ");
                Countdown(breatheOutSeconds); // Countdown for breathing out
                Console.WriteLine(); // Blank line for readability
            }
        }
    }
}
