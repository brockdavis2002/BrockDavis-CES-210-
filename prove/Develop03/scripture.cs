// Scripture.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scripture
{
    public string Reference { get; }
    private readonly List<Verse> _verses;

    public Scripture(string reference, List<string> verses)
    {
        Reference = reference;
        _verses = verses.Select(verse => new Verse(verse)).ToList();
    }

    public List<string> GetOriginalVerses()
    {
        return _verses.Select(verse => verse.OriginalText).ToList();
    }

    private Scripture()
    {
        Reference = "";
        _verses = new List<Verse>();
    }

    private Scripture(string reference, List<Verse> verses)
    {
        Reference = reference;
        _verses = verses;
    }

    public void HideRandomWords(int count = 1)
    {
        foreach (var verse in _verses)
        {
            verse.HideRandomWords(count);
        }
    }

    public string Display()
    {
        var versesDisplay = string.Join("\n", _verses.Select(verse => verse.Display()));
        return $"{Reference}\n{versesDisplay}";
    }

    public bool AllWordsHidden()
    {
        return _verses.All(verse => verse.AllWordsHidden());
    }

    public string Serialize()
    {
        var versesText = _verses.Any() ? string.Join("|", _verses.Select(verse => verse.Text)) : "";
        return $"{Reference}|{versesText}";
    }

    public static Scripture Deserialize(string data)
    {
        var parts = data.Split('|');
        var reference = parts[0];
        var verses = parts.Length > 1 ? parts[1].Split('|').ToList() : new List<string>();
        return new Scripture(reference, verses);
    }

    public static Scripture CreateScriptureFromUser()
    {
        Console.Write("Enter the scripture reference (e.g., 'John 3:16'): ");
        var reference = Console.ReadLine();
        var verses = new List<string>();
        while (true)
        {
            Console.Write("Enter a verse (or leave blank to finish): ");
            var verse = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(verse))
            {
                break;
            }
            verses.Add(verse);
        }
        return new Scripture(reference, verses);
    }
    public List<Verse> GetVerses()
{
    return _verses;
}

}
