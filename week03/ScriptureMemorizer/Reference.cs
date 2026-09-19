/// <summary>
/// Represents the reference of a scripture, such as "John 3:16" for a
/// single verse or "Proverbs 3:5-6" for a range of verses.
/// </summary>
public class Reference
{
    private readonly string _book;
    private readonly int _chapter;
    private readonly int _verse;
    private readonly int _endVerse;
    private readonly bool _isRange;

    /// <summary>Constructor for a reference to a single verse.</summary>
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
        _isRange = false;
    }

    /// <summary>Constructor for a reference to a range of verses.</summary>
    public Reference(string book, int chapter, int verse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = endVerse;
        _isRange = true;
    }

    public string GetDisplayText()
    {
        return _isRange
            ? $"{_book} {_chapter}:{_verse}-{_endVerse}"
            : $"{_book} {_chapter}:{_verse}";
    }
}
