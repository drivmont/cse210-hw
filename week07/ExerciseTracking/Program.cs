using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        // Show "Nov" and decimal points (3.0, not 3,0) on any computer, even one set to Spanish.
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        List<Activity> activities = new List<Activity>();

        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new Cycling(new DateTime(2022, 11, 4), 45, 15.0));
        activities.Add(new Swimming(new DateTime(2022, 11, 5), 40, 30));

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
