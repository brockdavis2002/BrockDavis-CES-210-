using System;

class Entry
{
    // Properties to store prompt, response, and date of the entry
    public string Prompt { get; } // Read-only property for the prompt
    public string Response { get; } // Read-only property for the response
    public string Date { get; set; } // Property for the date (with setter)
    public string Title { get; } // Read-only property for the title

    // Constructor to initialize the entry with prompt, response, date, and title
    public Entry(string prompt, string response, string date, string title)
    {
        Prompt = prompt; // Initialize the prompt property
        Response = response; // Initialize the response property
        Date = date; // Initialize the date property
        Title = title; // Initialize the title property
    }
}
