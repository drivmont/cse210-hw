public class Comment
{
    private readonly string _text;
    private readonly string _name;

    public Comment(string text, string name)
    {
        _text = text;
        _name = name;
    }

    public string GetDisplayText()
    {
        return $"{_text} - {_name}";
    }

    public string GetName()
    {
        return _name;
    }

    public string GetText()
    {
        return _text;
    }
}