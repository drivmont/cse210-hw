using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is the grade percentage you received in the class? ");
        int grade = int.Parse(Console.ReadLine());
        int numericGrade = grade%10;
        string letter;
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else if (grade < 60)
        {
            letter = "F";
        }
        else
        {
            letter = "Invalid grade";
        }
        if (numericGrade >= 7 && letter != "A" && letter != "F")
        {
            letter += "+";
        }
        else if (numericGrade <= 3 && letter != "F")
        {
            letter += "-";
        }
        Console.WriteLine($"Your letter grade is: {letter}");
        if (letter == "A" || letter == "B" || letter == "C")
        {
            Console.WriteLine("Congratulations! You passed the class.");
        }
        else if (letter == "D" || letter == "F")
        {
            Console.WriteLine("You did not pass the class. Better luck next time!");
        }
    }
}