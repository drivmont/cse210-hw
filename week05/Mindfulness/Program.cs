using System;

/*
 * EXCEEDING REQUIREMENTS
 * ----------------------
 * 1. No repeated prompts/questions: Reflecting and Listing never repeat a
 *    prompt or question until every one in the list has been used once in
 *    the session (Activity.GetRandomItem, shared by all activities).
 * 2. Session log: the program counts how many times each activity was done
 *    and the total seconds spent, and shows a summary when the user quits.
 * 3. Saving and loading a log file: every completed activity is appended to
 *    "mindfulness_log.txt". At startup the file is loaded and the program
 *    tells the user how many activities they completed in earlier sessions.
 * 4. Input validation: menu choices and durations are checked with TryParse
 *    so bad input asks again instead of crashing.
 */
class Program
{
    private const string LogFile = "mindfulness_log.txt";

    static void Main(string[] args)
    {
        Dictionary<string, int> sessionCounts = new Dictionary<string, int>();
        int sessionSeconds = 0;

        ShowPreviousSessions();

        string choice = "";
        while (choice != "4")
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            Activity completed = null;

            if (choice == "1")
            {
                BreathingActivity breathing = new BreathingActivity();
                breathing.Run();
                completed = breathing;
            }
            else if (choice == "2")
            {
                ReflectingActivity reflecting = new ReflectingActivity();
                reflecting.Run();
                completed = reflecting;
            }
            else if (choice == "3")
            {
                ListingActivity listing = new ListingActivity();
                listing.Run();
                completed = listing;
            }
            else if (choice != "4")
            {
                Console.WriteLine("Invalid choice. Please select 1-4.");
            }

            if (completed != null)
            {
                string name = completed.GetName();
                sessionCounts[name] = sessionCounts.GetValueOrDefault(name) + 1;
                sessionSeconds += completed.GetDuration();
                SaveToLog(name, completed.GetDuration());
                Console.Clear();
            }
        }

        ShowSessionSummary(sessionCounts, sessionSeconds);
    }

    static void ShowPreviousSessions()
    {
        Console.Clear();
        if (File.Exists(LogFile))
        {
            int previous = File.ReadAllLines(LogFile).Length;
            Console.WriteLine($"Welcome back! You have completed {previous} activities in previous sessions.");
            Console.WriteLine();
        }
    }

    static void SaveToLog(string activityName, int seconds)
    {
        string line = $"{DateTime.Now:yyyy-MM-dd HH:mm} | {activityName} | {seconds} seconds";
        File.AppendAllText(LogFile, line + Environment.NewLine);
    }

    static void ShowSessionSummary(Dictionary<string, int> counts, int totalSeconds)
    {
        Console.WriteLine();
        Console.WriteLine("Session summary:");
        if (counts.Count == 0)
        {
            Console.WriteLine("  No activities completed this time.");
        }
        foreach (KeyValuePair<string, int> entry in counts)
        {
            Console.WriteLine($"  {entry.Key}: {entry.Value} time(s)");
        }
        Console.WriteLine($"  Total mindful time: {totalSeconds} seconds");
        Console.WriteLine("Goodbye!");
    }
}
