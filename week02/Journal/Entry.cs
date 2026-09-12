public class Entry
{
    public string _promptText;
    public string _entryText;
    public string _date;

    public void Display()
    {
        Console.WriteLine($"DATE: {_date} - Prompt: {_promptText}");
        Console.WriteLine($"{_entryText}");
        Console.WriteLine();
    }
} 