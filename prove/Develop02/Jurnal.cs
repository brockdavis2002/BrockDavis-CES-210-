using System;
using System.Collections.Generic;

class Journal
{
    private List<Entry> entries; // List to store journal entries
    private List<string> prompts; // List to store prompts for journal entries

    public Journal()
    {
        entries = new List<Entry>();
        prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "What was the hardest and worst part of my day?",
            "What did you achieve today?",
            "What goals did you achieve today?",
            "What is something you learned today?"
            // Add more prompts here
        };
    }

    // Method to add a new entry to the journal
    public void AddEntry(string response, string title)
    {
        // Prompt selection randomizer picks a random prompt
        Random randomGenerator = new Random();
        int promptIndex = randomGenerator.Next(0, prompts.Count);

        if (promptIndex >= 0 && promptIndex < prompts.Count) // Validate prompt index
        {
            string prompt = prompts[promptIndex]; // Get selected prompt
            Console.WriteLine($"Prompt: {prompt}"); // Display selected prompt
            Console.Write("Enter your journal entry: "); // Prompt user for journal entry text
            string entryText = Console.ReadLine(); // Read user's journal entry
            string date = DateTime.Now.ToString("yyyy-MM-dd"); // Get current date
            Entry newEntry = new Entry(prompt, entryText, date, title); // Create new entry object
            entries.Add(newEntry); // Add new entry to the list of entries
            Console.WriteLine("Entry added successfully."); // Display success message
            Console.WriteLine(); // Add a blank line for spacing
        }
        else
        {
            Console.WriteLine("Invalid prompt index."); // Display error message for invalid prompt index
            Console.WriteLine(); // Add a blank line for spacing
        }
    }

    // Method to delete an entry from the journal
    public void DeleteEntry(int index)
    {
        if (index >= 0 && index < entries.Count) // Validate entry index
        {
            entries.RemoveAt(index); // Remove entry at the specified index
            Console.WriteLine("Entry deleted successfully."); // Display success message
            Console.WriteLine(); // Add a blank line for spacing
        }
        else
        {
            Console.WriteLine("Invalid entry index."); // Display error message for invalid entry index
            Console.WriteLine(); // Add a blank line for spacing
        }
    }

    // Method to display all entries in the journal
    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}"); // Display entry date
            Console.WriteLine($"Title: {entry.Title}"); // Display entry title
            Console.WriteLine($"Prompt: {entry.Prompt}"); // Display entry prompt
            Console.WriteLine($"Response: {entry.Response}\n"); // Display entry response
        }
    }

    // Method to list all titles in the journal
    public void ListTitles()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            Console.WriteLine($"{i}: {entries[i].Title}");
        }
        Console.WriteLine(); // Add a blank line for spacing
    }

    // Method to display a specific entry in the journal
    public void DisplayEntry(int index)
    {
        if (index >= 0 && index < entries.Count) // Validate entry index
        {
            Entry entry = entries[index]; // Get entry at the specified index
            Console.WriteLine($"Date: {entry.Date}"); // Display entry date
            Console.WriteLine($"Title: {entry.Title}"); // Display entry title
            Console.WriteLine($"Prompt: {entry.Prompt}"); // Display entry prompt
            Console.WriteLine($"Response: {entry.Response}\n"); // Display entry response
            Console.WriteLine(); // Add a blank line for spacing
        }
        else
        {
            Console.WriteLine("Invalid entry index."); // Display error message for invalid entry index
            Console.WriteLine(); // Add a blank line for spacing
        }
    }

    // Method to save journal entries to a file
    public void SaveJournal(string filename)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
        {
            foreach (var entry in entries) // Iterate through all entries in the list
            {
                file.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}|{entry.Title}"); // Write entry details to file
            }
        }
        Console.WriteLine("Journal saved successfully!"); // Display success message
        Console.WriteLine(); // Add a blank line for spacing
    }

    // Method to load journal entries from a file
    public void LoadJournal(string filename)
    {
        if (System.IO.File.Exists(filename)) // Check if the file exists
        {
            entries.Clear(); // Clear existing entries
            string[] lines = System.IO.File.ReadAllLines(filename); // Read all lines from the file
            foreach (string line in lines) // Iterate through each line
            {
                string[] parts = line.Split('|'); // Split line into parts using '|' as separator
                if (parts.Length >= 4) // Ensure enough parts to create an entry
                {
                    string date = parts[0]; // Extract date from parts
                    string prompt = parts[1]; // Extract prompt from parts
                    string response = parts[2]; // Extract response from parts
                    string title = parts[3]; // Extract title from parts
                    entries.Add(new Entry(prompt, response, date, title)); // Create new entry and add to the list
                }
                else
                {
                    Console.WriteLine($"Invalid entry format: {line}"); // Display error message for invalid entry format
                }
            }
            Console.WriteLine("Journal loaded successfully!"); // Display success message
            Console.WriteLine(); // Add a blank line for spacing
        }
        else
        {
            Console.WriteLine("File not found."); // Display error message if file does not exist
            Console.WriteLine(); // Add a blank line for spacing
        }
    }
}
