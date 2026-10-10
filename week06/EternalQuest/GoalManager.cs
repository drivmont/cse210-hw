public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    // Exceeding requirements: a simple level system based on score.
    private static readonly string[] _levelNames =
    {
        "Beginner", "Apprentice", "Explorer", "Adventurer", "Hero", "Champion", "Legend"
    };
    private const int PointsPerLevel = 500;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        string choice = "";
        while (choice != "6")
        {
            Console.WriteLine();
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine()?.Trim() ?? "6";

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoalDetails(); break;
                case "3": SaveGoals(); break;
                case "4": LoadGoals(); break;
                case "5": RecordEvent(); break;
                case "6": Console.WriteLine("Goodbye! Keep going on your quest."); break;
                default: Console.WriteLine("Please choose a number from 1 to 6."); break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        int level = Math.Min(_score / PointsPerLevel, _levelNames.Length - 1);
        int toNext = PointsPerLevel - (_score % PointsPerLevel);
        Console.WriteLine($"You have {_score} points.");
        Console.Write($"Level {level + 1}: {_levelNames[level]}");
        if (level < _levelNames.Length - 1)
        {
            Console.Write($" ({toNext} points to the next level)");
        }
        Console.WriteLine();
    }

    public void ListGoalNames()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
            return;
        }
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Negative Goal (a bad habit that costs points)");
        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine()?.Trim();
        if (type != "1" && type != "2" && type != "3" && type != "4")
        {
            Console.WriteLine("That is not a valid goal type.");
            return;
        }

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        int points = AskForNumber("What is the amount of points associated with this goal? ");

        if (type == "1")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (type == "4")
        {
            _goals.Add(new NegativeGoal(name, description, points));
        }
        else
        {
            int target = AskForNumber("How many times does this goal need to be accomplished for a bonus? ");
            int bonus = AskForNumber("What is the bonus for accomplishing it that many times? ");
            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
        Console.WriteLine("Goal created.");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
            return;
        }
        ListGoalNames();
        int index = AskForNumber("Which goal did you accomplish? ") - 1;
        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("That goal number does not exist.");
            return;
        }

        int oldLevel = _score / PointsPerLevel;
        int earned = _goals[index].RecordEvent();
        _score = Math.Max(0, _score + earned);

        if (earned > 0)
        {
            Console.WriteLine($"Congratulations! You have earned {earned} points!");
            Console.WriteLine($"You now have {_score} points.");
            if (_score / PointsPerLevel > oldLevel)
            {
                Console.WriteLine("*** LEVEL UP! ***");
            }
        }
        else if (earned < 0)
        {
            Console.WriteLine($"You lost {-earned} points. You now have {_score} points.");
        }
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("No filename given.");
            return;
        }

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals saved.");
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
        {
            Console.WriteLine("That file was not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] typeAndData = lines[i].Split(":", 2);
            string type = typeAndData[0];
            string[] parts = typeAndData[1].Split("|");

            string name = parts[0];
            string description = parts[1];
            int points = int.Parse(parts[2]);

            if (type == "SimpleGoal")
            {
                _goals.Add(new SimpleGoal(name, description, points, bool.Parse(parts[3])));
            }
            else if (type == "EternalGoal")
            {
                _goals.Add(new EternalGoal(name, description, points));
            }
            else if (type == "NegativeGoal")
            {
                _goals.Add(new NegativeGoal(name, description, points));
            }
            else if (type == "ChecklistGoal")
            {
                int bonus = int.Parse(parts[3]);
                int target = int.Parse(parts[4]);
                int amountCompleted = int.Parse(parts[5]);
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus, amountCompleted));
            }
        }
        Console.WriteLine("Goals loaded.");
    }

    private int AskForNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
            {
                return value;
            }
            Console.WriteLine("Please enter a whole number.");
        }
    }
}
