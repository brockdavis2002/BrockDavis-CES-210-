using System;

namespace MindfulnessApp
{
    public class Menu
    {
        public void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Mindfulness App!");
                Console.WriteLine("This app helps you relax and reflect through guided activities.");
                Console.WriteLine("Choose an activity to get started:");
                Console.WriteLine();

                Console.WriteLine("1. Breathing");
                Console.WriteLine("2. Reflecting");
                Console.WriteLine("3. Listing");
                Console.WriteLine("4. Quit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Breathing breathing = new Breathing();
                        breathing.StartActivity();
                        break;
                    case "2":
                        Reflecting reflecting = new Reflecting();
                        reflecting.StartActivity();
                        break;
                    case "3":
                        Listing listing = new Listing();
                        listing.StartActivity();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select again.");
                        break;
                }
                Console.WriteLine(); // Add a blank line after each selection for readability
            }
        }
    }
}
