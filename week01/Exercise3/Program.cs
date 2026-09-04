using System;

class Program
{
    static void Main(string[] args)
    {
        Boolean playAgain = true;
        while (playAgain)
        {
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 100);
            Console.Write("What is your guess? ");
            int guess = int.Parse(Console.ReadLine());
            int guesses = 1;
            while (guess != magicNumber)
            {
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guesses++;
            }
            Console.WriteLine("You guessed it!");
            Console.WriteLine($"It took you {guesses} guesses.");
            Console.Write("Do you want to play again? (y/n): ");
            string response = Console.ReadLine().ToLower();
            if (response != "y")
            {
                playAgain = false;
            }
        }
    }
}