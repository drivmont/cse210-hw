public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    // Used when loading from a file.
    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted)
        : this(name, description, points, target, bonus)
    {
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            Console.WriteLine("You already completed this goal.");
            return 0;
        }
        _amountCompleted++;
        if (IsComplete())
        {
            Console.WriteLine($"Bonus! You finished the checklist and earned {_bonus} extra points.");
            return GetPoints() + _bonus;
        }
        return GetPoints();
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        return $"{base.GetDetailsString()} Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{GetName()}|{GetDescription()}|{GetPoints()}|{_bonus}|{_target}|{_amountCompleted}";
    }
}
