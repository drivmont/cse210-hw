using System;

// Exceeding requirements:
// 1. A level system. Every 500 points the player levels up (Beginner up to Legend),
//    the current level and points to the next level are shown in the menu, and a
//    LEVEL UP message appears when a goal pushes the score into a new level.
// 2. A new goal type, NegativeGoal, for bad habits. Recording it takes points
//    away instead of adding them (the score never goes below zero).
// 3. Goals cannot be recorded again once they are complete, so points are not
//    given twice by mistake.
// 4. Number input is checked, so typing letters does not crash the program.

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}
