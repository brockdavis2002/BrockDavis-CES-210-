public class Verse
{
    public string Text { get; private set; }
    public string OriginalText { get; } // Property to store the original text
    public bool Hidden { get; private set; }

    public Verse(string text)
    {
        Text = text;
        OriginalText = text; // Initialize OriginalText with the provided text
        Hidden = false;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public void Show()
    {
        Hidden = false;
    }

    public string Display()
    {
        return Hidden ? "_____" : Text;
    }

    public void HideRandomWords(int count = 1)
    {
        var random = new Random();
        var wordsToHide = OriginalText.Split().Where(word => !string.IsNullOrWhiteSpace(word) && !word.Contains("_")).ToList();
        var newText = Text; // Create a copy of the current text

        for (int i = 0; i < Math.Min(count, wordsToHide.Count); i++)
        {
            var index = random.Next(0, wordsToHide.Count);
            newText = newText.Replace(wordsToHide[index], new string('_', wordsToHide[index].Length));
            wordsToHide.RemoveAt(index); // Remove the word that was hidden
        }

        Text = newText; // Update the Text property with the modified text
    }

    public bool AllWordsHidden()
    {
        return Text.Trim().Split().All(word => word.Contains("_"));
    }

    public void Reset()
    {
        Text = OriginalText; // Reset the text to its original state
        Hidden = false; // Reset hidden flag
    }
    
}
