using System.Text;

/// <summary>
/// Represents a single word within a scripture. A word knows its own
/// text and whether it is currently hidden, and knows how to render
/// itself either way.
/// </summary>
public class Word
{
    private readonly string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    /// <summary>
    /// Returns the text to display for this word. When hidden, every
    /// letter or digit is replaced with an underscore so the number of
    /// underscores matches the number of letters, while surrounding
    /// punctuation (commas, semicolons, periods, etc.) is left visible
    /// so the shape of the sentence is still recognizable.
    /// </summary>
    public string GetDisplayText()
    {
        if (!_isHidden)
        {
            return _text;
        }

        StringBuilder hiddenText = new StringBuilder(_text.Length);
        foreach (char character in _text)
        {
            hiddenText.Append(char.IsLetterOrDigit(character) ? '_' : character);
        }

        return hiddenText.ToString();
    }
}
