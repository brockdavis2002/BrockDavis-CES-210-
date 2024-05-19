using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a new instance of the Journal class
        Journal journal = new Journal();

        // Automatically load the journal entries from a default file
        string defaultFilename = "journal.txt";
        journal.LoadJournal(defaultFilename);

        // Variable to control the loop
        bool exit = false;

        // Main loop to display the menu and handle user input
        while (!exit)
        {
            // Display the menu options
            Console.WriteLine("Welcome to Journal.com");
            Console.WriteLine("1. Add Entry");
            Console.WriteLine("2. Delete Entry");
            Console.WriteLine("3. Display Journal");
            Console.WriteLine("4. Display Entry");
            Console.WriteLine("5. Export Journal");
            Console.WriteLine("6. Load Journal");
            Console.WriteLine("7. Save and Exit");
            Console.WriteLine();
            Console.Write("Select an option: ");
            string choice = Console.ReadLine(); // Read user's choice
            Console.WriteLine(); // Add a blank line for spacing

            // Switch statement to execute different actions based on user's choice
            switch (choice)
            {
                case "1": // Add Entry
                    Console.Write("Enter Today's title: ");
                    string title = Console.ReadLine();
                    Console.WriteLine(); // Add a blank line for spacing
                    journal.AddEntry(title, title); // Call AddEntry method of the Journal class
                    break;

                case "2": // Delete Entry
                    journal.ListTitles(); // List all titles
                    Console.Write("Enter index of entry to delete: ");
                    int deleteIndex = int.Parse(Console.ReadLine());
                    Console.WriteLine(); // Add a blank line for spacing
                    journal.DeleteEntry(deleteIndex); // Call DeleteEntry method of the Journal class
                    break;

                case "3": // Display Journal
                    journal.DisplayJournal(); // Call DisplayJournal method of the Journal class
                    Console.WriteLine(); // Add a blank line for spacing
                    break;

                case "4": // Display Entry
                    journal.ListTitles(); // List all titles
                    Console.Write("Enter index of entry to display: ");
                    int displayIndex = int.Parse(Console.ReadLine());
                    Console.WriteLine(); // Add a blank line for spacing
                    journal.DisplayEntry(displayIndex); // Call DisplayEntry method of the Journal class
                    break;

                case "5": // Save Journal
                    Console.Write("Enter filename to export journal incude file format: ");
                    string saveFilename = Console.ReadLine();
                    Console.WriteLine(); // Add a blank line for spacing
                    journal.SaveJournal(saveFilename); // Call SaveJournal method of the Journal class
                    break;

                case "6": // Load Journal
                    Console.Write("Enter filename to load journal incude format: ");
                    string loadFilename = Console.ReadLine();
                    Console.WriteLine(); // Add a blank line for spacing
                    journal.LoadJournal(loadFilename); // Call LoadJournal method of the Journal class
                    break;

                case "7": // Exit
                    // Automatically save the journal to the default file before exiting
                    journal.SaveJournal(defaultFilename);
                    exit = true; // Set exit flag to true to exit the loop
                    break;

                default: // Invalid option
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine(); // Add a blank line for spacing
                    break;
            }
        }
    }
}
