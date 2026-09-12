public class Journal
{
    public List<Entry> _entries = new List<Entry>();
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void Display()
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            Console.WriteLine($"Entry {i + 1}:");
            _entries[i].Display();
            Console.WriteLine();
        }
    }

    public void SaveToFile(string filename)
    {
        if (File.Exists(filename))
        {
            Console.Write($"'{filename}' already exists. Overwrite? (y/n) ");
            string response = Console.ReadLine();
            if (response == null || response.Trim().ToLower() != "y")
            {
                Console.WriteLine("Save cancelled.");
                return;
            }
        }

        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                foreach (Entry entry in _entries)
                {
                    outputFile.WriteLine(entry._date);
                    outputFile.WriteLine(entry._promptText);
                    outputFile.WriteLine(entry._entryText);
                }
            }
            Console.WriteLine($"Saved to '{filename}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not save file: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File '{filename}' does not exist.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            _entries.Clear(); // optional: avoid appending to whatever was already loaded

            for (int i = 0; i + 2 < lines.Length; i += 3)
            {
                Entry entry = new Entry();
                entry._date = lines[i];
                entry._promptText = lines[i + 1];
                entry._entryText = lines[i + 2];
                _entries.Add(entry);
            }

            Console.WriteLine($"Loaded {_entries.Count} entries from '{filename}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not load file: {ex.Message}");
        }
    }

}
