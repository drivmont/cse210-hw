public class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("Breathe in...");
            ShowCountDown(Math.Min(4, SecondsLeft(endTime)));
            Console.WriteLine();

            if (SecondsLeft(endTime) <= 0)
            {
                break;
            }

            Console.Write("Now breathe out...");
            ShowCountDown(Math.Min(6, SecondsLeft(endTime)));
            Console.WriteLine();
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }

    private int SecondsLeft(DateTime endTime)
    {
        return (int)Math.Ceiling((endTime - DateTime.Now).TotalSeconds);
    }
}
