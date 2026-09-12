public class promptGenerator
{
    public List<string> _prompts;

    string filename = "prompts.txt";

    // Used only if prompts.txt is missing or can't be read, so the program
    // never crashes just because that file isn't where it expects it.
    private static readonly List<string> _defaultPrompts = new List<string>
    {
        "Who was the most interesting person you interacted with today?",
        "What was the best part of your day?",
        "What was the strongest emotion you felt today?"
    };

    private void LoadPromptsFromFile(string filename)
    {
        if (!System.IO.File.Exists(filename))
        {
            Console.WriteLine($"Warning: prompt file '{filename}' was not found. Using default prompts instead.");
            _prompts = new List<string>(_defaultPrompts);
            return;
        }

        try
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            if (lines.Length == 0)
            {
                Console.WriteLine($"Warning: prompt file '{filename}' is empty. Using default prompts instead.");
                _prompts = new List<string>(_defaultPrompts);
                return;
            }

            _prompts = new List<string>(lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: could not read '{filename}' ({ex.Message}). Using default prompts instead.");
            _prompts = new List<string>(_defaultPrompts);
        }
    }

    public string GetRandomPrompt()
    {
        Random _random = new Random();
        LoadPromptsFromFile(filename);
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
    
    public void DisplayPrompt()
    {
        LoadPromptsFromFile(filename);
        for (int i = 0; i < _prompts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_prompts[i]}");
        }
    }
}