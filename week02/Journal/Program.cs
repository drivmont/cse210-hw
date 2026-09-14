using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        Journal _journal = new Journal();
        PromptGenerator _promptGenerator = new PromptGenerator();
        bool running = true;

        // Exceeds requirements: the program does not crash on bad input or
        // missing files. Menu input is validated with int.TryParse instead of
        // int.Parse, saving warns and asks for confirmation before it
        // overwrites an existing file, loading checks that the file exists
        // before reading it, and PromptGenerator falls back to a small
        // built in list of prompts if prompts.txt is missing, empty, or
        // cannot be read, instead of throwing an unhandled exception.

        while (running)
        {
            Console.WriteLine("Welcome to the Journal Program!");
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection))
            {
                Console.WriteLine("Please enter a valid number.");
                Console.WriteLine();
                continue;
            }

            if (selection == 1)
            {
                string prompt = _promptGenerator.GetRandomPrompt();
                Console.WriteLine(prompt);
                Console.Write("> ");
                Entry _entry = new Entry();
                _entry._entryText = Console.ReadLine();
                _entry._promptText = prompt;
                _entry._date = DateTime.Now.ToString("MM/dd/yyyy");
                _journal.AddEntry(_entry);
            }
            else if (selection == 2)
            {
                _journal.Display();
            }
            else if (selection == 3)
            {
                Console.WriteLine("What is the file name you want to load?");
                string filename = Console.ReadLine();
                _journal.LoadFromFile(filename);
            }
            else if (selection == 4)
            {
                Console.WriteLine("What is the file name you want to save to?");
                string filename = Console.ReadLine();
                _journal.SaveToFile(filename);
            }
            else if (selection == 5)
            {
                running = false;
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            Console.WriteLine(); // blank line for readability between loops
        }

        Console.WriteLine("Goodbye!");
    }
}