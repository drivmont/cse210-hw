using System;

// =====================================================================
// EXCEEDING THE REQUIREMENTS
// =====================================================================
// This program goes beyond the core assignment in the following ways:
//
//   1. Library of scriptures (ScriptureLibrary.cs)
//
//   2. Loading scriptures from a file
//
//   3. Only hiding words that are not already hidden 
//
//   4. Punctuation-aware hiding (Word.GetDisplayText): when a word is
//      hidden, only its letters and digits become underscores; commas,
//      semicolons, periods, etc. stay visible.
//
//   5. Progress feedback: after the scripture text, the program shows
//      how many words are still visible, so the user can see how close
//      they are to having the whole verse memorized. 
//
//   6. Practicing more than one scripture in a sitting: once a
//      scripture is fully hidden, the user is asked whether to practice
//      another randomly chosen scripture from the library, instead of
//      the program simply ending. 
// =====================================================================

class Program
{
    private const string _scriptureFileName = "scriptures.txt";
    private static readonly Random _random = new Random();

    static void Main(string[] args)
    {
        ScriptureLibrary library = new ScriptureLibrary(_scriptureFileName);

        bool keepPracticing = true;
        while (keepPracticing)
        {
            Scripture scripture = library.GetRandomScripture();
            Practice(scripture);
            keepPracticing = PromptToContinue();
        }

        Console.WriteLine("Goodbye!");
    }

    // Repeatedly hides a few more words and redisplays the scripture
    // until either the whole verse is hidden or the user quits.
    private static void Practice(Scripture scripture)
    {
        while (!scripture.IsCompletelyHidden())
        {
            DisplayScripture(scripture);

            Console.WriteLine();
            Console.Write("Press enter to continue or type 'quit' to exit: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (input.Trim().ToLower() == "quit")
            {
                Environment.Exit(0);
            }

            int numberToHide = _random.Next(2, 5);
            scripture.HideRandomWords(numberToHide);
        }

        DisplayScripture(scripture);
    }

    private static void DisplayScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine($"({scripture.CountVisibleWords()} word(s) still visible)");
    }

    private static bool PromptToContinue()
    {
        Console.WriteLine();
        Console.Write("Scripture memorized! Practice another? (y/n): ");
        string input = Console.ReadLine() ?? string.Empty;
        return input.Trim().ToLower().StartsWith("y");
    }
}
