public class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        Hidden = false;//returns if not hiden
    }

    public void Hide()
    {
        Hidden = true;//returns if hiden
    }

    public void Show()
    {
        Hidden = false;//returns false if not hiden
    }

    public string Display()
    {
        return Hidden ? "_____" : Text;//returns___ if hiden
    }
}
