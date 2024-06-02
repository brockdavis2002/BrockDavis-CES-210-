using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    // File to store scriptures
    private const string ScripturesFile = "scriptures.txt";

    // List to store loaded scriptures
    private readonly List<Scripture> _scriptures;

    // Constructor to initialize the program
    public Program()
    {
        // Load scriptures from the file
        _scriptures = LoadScriptures();
    }

    // Method to clear the console screen
    private void ClearScreen()
    {
        Console.Clear();
    }

    // Method to load scriptures from the file
    private List<Scripture> LoadScriptures()
    {
        // Check if the file exists
        if (!File.Exists(ScripturesFile))
        {
            // If file doesn't exist, return an empty list
            return new List<Scripture>();
        }

        // Read all lines from the file and deserialize them into scripture objects
        var serializedScriptures = File.ReadAllLines(ScripturesFile);
        return serializedScriptures.Select(serializedScripture => Scripture.Deserialize(serializedScripture)).ToList();
    }

    // Method to save scriptures to the file
    private void SaveScriptures()
    {
        // Serialize the scripture objects and write them to the file
        var serializedScriptures = _scriptures.Select(scripture => scripture.Serialize());
        File.WriteAllLines(ScripturesFile, serializedScriptures);
    }

    // Method to run a scripture
    private void RunScripture(Scripture scripture)
    {
        var originalScripture = new Scripture(scripture.Reference, scripture.GetOriginalVerses());
        var random = new Random();

        // Main loop to run the scripture
        while (!scripture.AllWordsHidden())
        {
            ClearScreen();
            Console.WriteLine(scripture.Display());
            Console.Write("Press Enter to hide 1 to 3 words or type 'quit' to exit: ");
            var userInput = Console.ReadLine()?.Trim().ToLower();

            if (userInput == "quit")
            {
                return;
            }
            else
            {
                var wordsToHideCount = random.Next(1, 4);
                for (int i = 0; i < wordsToHideCount; i++)
                {
                    scripture.HideRandomWords();
                }
            }
        }

        // Display completion message when all words are hidden
        ClearScreen();
        Console.WriteLine(scripture.Display());
        Console.Write("Press Enter to continue...");
        Console.ReadLine();

        ClearScreen();
        Console.WriteLine("All words hidden. Good job!");
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    // Method to run the program
    public void Run()
    {
        // Main loop to display the menu and handle user input
        while (true)
        {
            ClearScreen();
            Console.WriteLine("Scripture Memorizer");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Run an old scripture");
            Console.WriteLine("2. Create a new scripture");
            Console.WriteLine("3. Exit program");
            Console.Write("Enter your choice (1, 2, or 3): ");
            var choice = Console.ReadLine()?.Trim();

            // Handling user choice
            if (choice == "1")
            {
                // Check if there are old scriptures available
                if (_scriptures.Count == 0)
                {
                    Console.WriteLine("No old scriptures found.");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    continue;
                }

                // Display old scriptures and prompt for selection
                Console.WriteLine("Old scriptures:");
                for (int i = 0; i < _scriptures.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_scriptures[i].Reference}");
                }

                Console.Write("Enter the number of the scripture to run: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _scriptures.Count)
                {
                    RunScripture(_scriptures[index - 1]);
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }

                // Reset the verses before going back to the main menu
                foreach (var scripture in _scriptures)
                {
                    foreach (var verse in scripture.GetVerses())
                    {
                        verse.Reset();
                    }
                }

            }
            else if (choice == "2")
            {
                // Create a new scripture from user input
                var newScripture = Scripture.CreateScriptureFromUser();
                _scriptures.Add(newScripture);
                SaveScriptures();
                RunScripture(newScripture);
            }
            else if (choice == "3")
            {
                // Exit program
                Console.WriteLine("Exiting program...");
                return;
            }
            else
            {
                // Invalid choice
                Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    // Main method to start the program
    public static void Main(string[] args)
    {
        var program = new Program();
        program.Run();
    }
}
