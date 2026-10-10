// Extra goal type: a bad habit to avoid. Recording it takes points away.
public class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        Console.WriteLine("Oh no! That habit costs you points. You can do better next time.");
        return -GetPoints();
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[!] {GetName()} ({GetDescription()}) Lose {GetPoints()} points each time";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{GetName()}|{GetDescription()}|{GetPoints()}";
    }
}
